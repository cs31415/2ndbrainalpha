﻿using System.Drawing;

namespace ScintillaNET.Demo
{
	public class SciTextBox : Scintilla
    {
		/// <summary>
		/// change this to whatever margin you want the line numbers to show in
		/// </summary>
		private const int NUMBER_MARGIN = 1;

		/// <summary>
		/// change this to whatever margin you want the bookmarks/breakpoints to show in
		/// </summary>
		private const int BOOKMARK_MARGIN = 2;
		private const int BOOKMARK_MARKER = 2;

		public SciTextBox()
        {
			Dock = System.Windows.Forms.DockStyle.Fill;

			// INITIAL VIEW CONFIG
			WrapMode = WrapMode.Word;
			IndentationGuides = IndentView.LookBoth;

			InitColors();
			InitSyntaxColoring();
			InitNumberMargin();
		}

        private void InitColors()
        {
            SetSelectionBackColor(true, Color.White);
        }

		private void InitSyntaxColoring()
		{
			// Configure the default style
			StyleResetDefault();
			Styles[Style.Default].Font = "Lucida Console";
			Styles[Style.Default].Size = 10;
			Styles[Style.Default].BackColor = Color.White;
			Styles[Style.Default].ForeColor = Color.Black;
			StyleClearAll();
		}

		private void InitNumberMargin()
		{

			Styles[Style.LineNumber].BackColor = Color.LightGray;
			//TextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
			//TextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

			var nums = Margins[NUMBER_MARGIN];
			nums.Width = 30;
			nums.Type = MarginType.Number;
			nums.Sensitive = true;
			nums.Mask = 0;

			MarginClick += TextArea_MarginClick;
		}

		private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
		{
			if (e.Margin == BOOKMARK_MARGIN)
			{
				// Do we have a marker for this line?
				const uint mask = (1 << BOOKMARK_MARKER);
				var line = Lines[LineFromPosition(e.Position)];
				if ((line.MarkerGet() & mask) > 0)
				{
					// Remove existing bookmark
					line.MarkerDelete(BOOKMARK_MARKER);
				}
				else
				{
					// Add bookmark
					line.MarkerAdd(BOOKMARK_MARKER);
				}
			}
		}
	}
}