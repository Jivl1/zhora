using System;
using System.Drawing;
using System.Windows.Forms;

namespace Лр3
{
    public class MainForm : Form
    {
        private RichTextBox richTextBox1;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel positionLabel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Блокнот";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;

            var menu = new MenuStrip();

            var fileMenu = new ToolStripMenuItem("Файл");
            var saveMenu = new ToolStripMenuItem("Зберегти");
            var exitMenu = new ToolStripMenuItem("Вихід");
            saveMenu.ShortcutKeys = Keys.Control | Keys.S;
            saveMenu.Click += saveToolStripMenuItem_Click;
            exitMenu.Click += exitToolStripMenuItem_Click;
            fileMenu.DropDownItems.Add(saveMenu);
            fileMenu.DropDownItems.Add(exitMenu);

            var editMenu = new ToolStripMenuItem("Редагування");
            var copyMenu = new ToolStripMenuItem("Копіювати");
            var cutMenu = new ToolStripMenuItem("Вирізати");
            var pasteMenu = new ToolStripMenuItem("Вставити");
            copyMenu.Click += copyToolStripMenuItem_Click;
            cutMenu.Click += cutToolStripMenuItem_Click;
            pasteMenu.Click += pasteToolStripMenuItem_Click;
            editMenu.DropDownItems.Add(copyMenu);
            editMenu.DropDownItems.Add(cutMenu);
            editMenu.DropDownItems.Add(pasteMenu);

            var helpMenu = new ToolStripMenuItem("Довідка");
            var aboutMenu = new ToolStripMenuItem("Про програму");
            aboutMenu.Click += aboutToolStripMenuItem_Click;
            helpMenu.DropDownItems.Add(aboutMenu);

            menu.Items.Add(fileMenu);
            menu.Items.Add(editMenu);
            menu.Items.Add(helpMenu);

            var toolStrip = new ToolStrip();
            var saveButton = IconButton("Зберегти", Icons.Save);
            var copyButton = IconButton("Копіювати", Icons.Copy);
            var cutButton = IconButton("Вирізати", Icons.Cut);
            var pasteButton = IconButton("Вставити", Icons.Paste);
            saveButton.Click += saveToolStripButton_Click;
            copyButton.Click += copyToolStripButton_Click;
            cutButton.Click += cutToolStripButton_Click;
            pasteButton.Click += pasteToolStripButton_Click;
            toolStrip.Items.Add(saveButton);
            toolStrip.Items.Add(copyButton);
            toolStrip.Items.Add(cutButton);
            toolStrip.Items.Add(pasteButton);

            richTextBox1 = new RichTextBox { Dock = DockStyle.Fill };

            var context = new ContextMenuStrip();
            var copyContext = new ToolStripMenuItem("Копіювати");
            var cutContext = new ToolStripMenuItem("Вирізати");
            var pasteContext = new ToolStripMenuItem("Вставити");
            copyContext.Click += copyContextMenuItem_Click;
            cutContext.Click += cutContextMenuItem_Click;
            pasteContext.Click += pasteContextMenuItem_Click;
            context.Items.Add(copyContext);
            context.Items.Add(cutContext);
            context.Items.Add(pasteContext);
            richTextBox1.ContextMenuStrip = context;

            var status = new StatusStrip();
            var assignmentLabel = new ToolStripStatusLabel("ЛР3");
            statusLabel = new ToolStripStatusLabel("Кількість символів: 0");
            positionLabel = new ToolStripStatusLabel("Рядок: 1, Стовпець: 1")
            {
                Spring = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            status.Items.Add(assignmentLabel);
            status.Items.Add(statusLabel);
            status.Items.Add(positionLabel);

            Controls.Add(richTextBox1);
            Controls.Add(toolStrip);
            Controls.Add(status);
            Controls.Add(menu);
            MainMenuStrip = menu;

            richTextBox1.TextChanged += richTextBox1_TextChanged;
            richTextBox1.SelectionChanged += richTextBox1_SelectionChanged;
            Shown += MainForm_Shown;
        }

        private static ToolStripButton IconButton(string text, Image image) =>
            new ToolStripButton(text, image)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                ToolTipText = text
            };

        private void MainForm_Shown(object sender, EventArgs e)
        {
            richTextBox1.Select();
            richTextBox1.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Зберегти файл"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.SaveFile(dialog.FileName, RichTextBoxStreamType.PlainText);
                    MessageBox.Show("Файл успішно збережено!", "Збереження",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0) richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                    richTextBox1.SelectedText = Clipboard.GetText();
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Буфер обміну зараз недоступний.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e) =>
            saveToolStripMenuItem_Click(sender, e);

        private void copyToolStripButton_Click(object sender, EventArgs e) =>
            copyToolStripMenuItem_Click(sender, e);

        private void cutToolStripButton_Click(object sender, EventArgs e) =>
            cutToolStripMenuItem_Click(sender, e);

        private void pasteToolStripButton_Click(object sender, EventArgs e) =>
            pasteToolStripMenuItem_Click(sender, e);

        private void copyContextMenuItem_Click(object sender, EventArgs e) =>
            copyToolStripMenuItem_Click(sender, e);

        private void cutContextMenuItem_Click(object sender, EventArgs e) =>
            cutToolStripMenuItem_Click(sender, e);

        private void pasteContextMenuItem_Click(object sender, EventArgs e) =>
            pasteToolStripMenuItem_Click(sender, e);

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            statusLabel.Text = $"Кількість символів: {richTextBox1.Text.Length}";
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            int line = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
            int first = richTextBox1.GetFirstCharIndexFromLine(line - 1);
            if (first < 0)
                first = richTextBox1.TextLength;

            int column = richTextBox1.SelectionStart - first + 1;
            positionLabel.Text = $"Рядок: {line}, Стовпець: {column}";
        }
    }
}
