using System;
using System.Windows.Forms;

namespace Text_Editor
{
    public class FixedRichTextBox : RichTextBox
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!base.AutoWordSelection)
            {
                base.AutoWordSelection = true;
                base.AutoWordSelection = false;
            }
        }
    }
}
