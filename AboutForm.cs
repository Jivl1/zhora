using System.Drawing;
using System.Windows.Forms;

namespace Лр3
{
    public class AboutForm : Form
    {
        public AboutForm()
        {
            Text = "Про програму";
            ClientSize = new Size(380, 290);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            var picture = new PictureBox
            {
                Image = Icons.Notepad,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 20),
                Size = new Size(64, 64)
            };

            var titleLabel = new Label
            {
                Text = "Блокнот v1.0",
                Font = new Font(Font.FontFamily, 14f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 18)
            };

            var developerLabel = new Label
            {
                Text = "Розробник: Стекольщіков Георгій Олегович",
                AutoSize = true,
                Location = new Point(102, 48)
            };

            var groupLabel = new Label
            {
                Text = "Група: 491",
                AutoSize = true,
                Location = new Point(102, 68)
            };

            var yearLabel = new Label
            {
                Text = "2026",
                AutoSize = true,
                Location = new Point(102, 88)
            };

            var description = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White,
                Location = new Point(20, 120),
                Size = new Size(340, 110),
                Text = "Ця програма є навчальним проєктом.\r\n" +
                       "Основні функції:\r\n" +
                       "• Робота з текстом\r\n" +
                       "• Копіювання, вирізання та вставка\r\n" +
                       "• Збереження файлів\r\n" +
                       "• Контекстне меню\r\n" +
                       "• Панель інструментів\r\n" +
                       "• Рядок стану"
            };

            var okButton = new Button
            {
                Text = "OK",
                Location = new Point(145, 245),
                Size = new Size(90, 28)
            };
            okButton.Click += (s, e) => Close();
            AcceptButton = okButton;
            CancelButton = okButton;

            Controls.AddRange(new Control[]
            {
                picture, titleLabel, developerLabel, groupLabel, yearLabel, description, okButton
            });
            Shown += (s, e) => okButton.Focus();
        }
    }
}
