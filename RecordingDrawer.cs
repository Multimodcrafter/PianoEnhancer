using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PianoEnhancer
{
    internal class RecordingDrawer
    {
        public RecordingDrawer()
        {
            CurrentPage = 0;
            PageCount = 0;
        }

        public int PageCount { get; private set; }
        public int CurrentPage { get; set; }
        public TrackRecorder Recorder { get; set; }
        public PlayBack PlayBack { get; set; }

        private IList<Chord> _chords;
        private Dictionary<int, VoiceComposition> _switchPoints;

        private readonly Pen _linePen = new Pen(Color.Black);
        private readonly Pen _recordingPen = new Pen(Color.Red,3);
        private readonly Pen _playBackPen = new Pen(Color.Green,3);
        private readonly Brush _noteBrush = new SolidBrush(Color.Black);
        private readonly Brush _voiceChangeBrush = new SolidBrush(Color.DodgerBlue);
        private readonly Font _sharpFont = new Font(FontFamily.GenericMonospace, BaseLineDistance);
        //how far apart each note line should be and how big notes should be
        private const int BaseLineDistance = 10;
        //distance between high and bass baseLines is equal to 6 gaps (4 gaps in high base line and 2 inter base line gaps)
        private const int BaseLineInterDistance = BaseLineDistance * 6;
        //save area around everything
        private const int PanelPadding = 10;
        //distance from the top to the high base line (starting at F5 from the top) is 18 notes
        //(C8 is the highest note) which yields 18/2 gaps
        private const int BaseLinePadding = 18/2 * BaseLineDistance;
        //height of one full base line pair (including free space for notes exceeding the base lines)
        private const int BaseLineHeight = BaseLinePadding * 2 + 10 * BaseLineDistance;
        private const int HorizontalChordDistance = 20;
        //midi number for the lowest supported note
        private const int NoteIndexOffset = 21;
        //total note count in range (excluding black keys)
        private const int NoteRange = 50;


        public void HandleClick(int x, int y, Size size, MouseButtons button)
        {
            var baseLineCount = (size.Height - 2 * PanelPadding) / BaseLineHeight;
            var chordCountPerLine = (size.Width - 2 * PanelPadding) / HorizontalChordDistance;
            var totalChordsPerPage = chordCountPerLine * baseLineCount;
            var clickedLine = y / BaseLineHeight;
            var clickedChordOnPage = clickedLine * chordCountPerLine + (x - PanelPadding) / HorizontalChordDistance;
            var finalChordIndex = CurrentPage * totalChordsPerPage + clickedChordOnPage;
            if (finalChordIndex >= _chords.Count) finalChordIndex = _chords.Count - 1;
            if (finalChordIndex < 0) finalChordIndex = 0;
            switch (button)
            {
                case MouseButtons.Left:
                {
                    using var voiceDialog = new VoiceSelector();
                    if (voiceDialog.ShowDialog() == DialogResult.OK)
                    {
                        Recorder.Recording.SetVoice(finalChordIndex, voiceDialog.Voice.ToVoiceComposition());
                    }

                    break;
                }
                case MouseButtons.Right when _switchPoints.ContainsKey(finalChordIndex):
                    Recorder.Recording.RemoveVoice(finalChordIndex);
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    return;
            }
        }

        public void DrawPanel(Graphics g, Size size)
        {
            _chords = Recorder.Recording.Track;
            _switchPoints = Recorder.Recording.GetSwitchPoints().ToDictionary(x => x.Key, y => y.Value);
            var baseLineCount = (size.Height - 2 * PanelPadding) / BaseLineHeight;
            for (var i = 0; i < baseLineCount; ++i)
            {
                DrawBaseLines(PanelPadding + i * BaseLineHeight, size.Width, g);
            }

            var chordCountPerLine = (size.Width - 2 * PanelPadding) / HorizontalChordDistance;
            var totalChordsPerPage = chordCountPerLine * baseLineCount;
            PageCount = totalChordsPerPage > 0 ? (int)Math.Ceiling((double)_chords.Count / totalChordsPerPage) : 0;
            if (CurrentPage >= PageCount) CurrentPage = PageCount - 1;
            if (CurrentPage < 0) CurrentPage = 0;
            DrawChords(totalChordsPerPage,chordCountPerLine,baseLineCount,g);
        }

        private void DrawBaseLines(int y, int width, Graphics g)
        {
            y += BaseLinePadding;
            for (var i = 0; i < 5; ++i)
            {
                g.DrawLine(_linePen, PanelPadding, y + i * BaseLineDistance, width - PanelPadding, y + i * BaseLineDistance);
                g.DrawLine(_linePen,PanelPadding,y+BaseLineInterDistance + i * BaseLineDistance,width - PanelPadding, y + BaseLineInterDistance + i * BaseLineDistance);
            }
        }

        private void DrawChords(int chordsPerPage, int chordsPerLine,int lineCount,Graphics g)
        {
            var offset = CurrentPage * chordsPerPage;
            for (var i = 0; i < lineCount; ++i)
            {
                for (var j = 0; j < chordsPerLine; ++j)
                {
                    var x = j * HorizontalChordDistance + PanelPadding + BaseLineDistance/2;
                    var index = offset + i * chordsPerLine + j;
                    if (index >= _chords.Count)
                    {
                        if (Recorder.IsRecording) DrawCursor(x,i,_recordingPen,g);
                        return;
                    }

                    foreach (var note in _chords[index].Notes)
                    {
                        var correctedNote = note - NoteIndexOffset;
                        var noteSteps = Voices.NoteNames.Take(correctedNote).Count(n => !n.Contains("#") && !n.Contains("B"));
                        if (Voices.NoteNames[correctedNote].Contains("#")) --noteSteps;
                        var y = PanelPadding + i * BaseLineHeight + (NoteRange - noteSteps) * (BaseLineDistance/2);
                        DrawNote(x,y,g,correctedNote,_switchPoints.ContainsKey(index) ? _voiceChangeBrush : _noteBrush);
                    }
                    if(PlayBack.PendingPosition == index && PlayBack.IsPlaying) DrawCursor(x,i,_playBackPen,g);
                }
            }
        }

        private static void DrawCursor(int x, int line, Pen pen, Graphics g)
        {
            g.DrawLine(pen,x,PanelPadding + line * BaseLineHeight,x,PanelPadding + (line+1)*BaseLineHeight);
        }

        private void DrawNote(int x, int y, Graphics g, int note, Brush b)
        {
            g.FillEllipse(b,x,y,BaseLineDistance,BaseLineDistance);
            if(Voices.NoteNames[note].Contains("#"))
                g.DrawString("#",_sharpFont,b,(int)(x-BaseLineDistance*1.2),y-BaseLineDistance/2);
            else if(Voices.NoteNames[note].Contains("B"))
                g.DrawString("b",_sharpFont,b,(int)(x-BaseLineDistance*1.2),y-BaseLineDistance/2);
            if(note == 39 || note == 40)
            {
                g.DrawLine(_linePen,x-BaseLineDistance/2,y + BaseLineDistance /2,(int)(x+BaseLineDistance*1.5),y + BaseLineDistance/2);
            } else if (note >= 59)
            {
                var steps = Voices.NoteNames.Take(note).Count(n => !n.Contains("#") && !n.Contains("B")) - 33;
                var helpCount = steps / 2;
                for (var line = 0; line < helpCount; ++line)
                {
                    var newY = (int) (y + (line + 0.5) * BaseLineDistance) + (steps % 2 == 0 ? 0: BaseLineDistance / 2);
                    g.DrawLine(_linePen,x-BaseLineDistance/2,newY,(int)(x +BaseLineDistance*1.5),newY);
                }
            } else if(note <= 19)
            {
                var steps = 13 - Voices.NoteNames.Take(note).Count(n => !n.Contains("#") && !n.Contains("B"));
                var helpCount = steps / 2;
                for (var line = 0; line < helpCount; ++line)
                {
                    var newY = (int) (y - (line - 0.5) * BaseLineDistance) - (steps % 2 == 0 ? 0: BaseLineDistance / 2);
                    g.DrawLine(_linePen, x - BaseLineDistance / 2, newY, (int) (x + BaseLineDistance * 1.5), newY);
                }
            }
        }
    }
}
