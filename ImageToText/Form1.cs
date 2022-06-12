namespace ImageToText
{
    public partial class Form1 : Form
    {
        string whitePixel; // Symbols replacing the white pixel
        string blackPixel; // Symbols replacing the black pixel
        Bitmap image;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opening and preparation of data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = image;

                if (image.Height > 20 || image.Width > 20)
                {
                    MessageBox.Show("The maximum size of images is 20x20 px", "ERROR", MessageBoxButtons.OK);
                    pictureBox1.Image = null;
                }
            }
        }

        /// <summary>
        /// Reading the pixel color of the uploaded image
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns></returns>
        private Color [,] GetPixels(Bitmap image)
        {
            var pixelsColor = new Color [image.Height, image.Width];
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                    pixelsColor [y, x] = image.GetPixel (x, y);
            return pixelsColor;
        }

        /// <summary>
        /// Writing symbols of array in the right order
        /// </summary>
        /// <param name="text">Array</param>
        /// <returns></returns>
        private string WriteArray (string [,] text)
        {
            string textFromArray = "";
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    textFromArray += text[y, x];
                }
                textFromArray += "\r" + "\n";
            }
            return textFromArray;
        }

        /// <summary>
        /// Converting text to image
        /// </summary>
        /// <param name="white">Replacement for white color</param>
        /// <param name="black">Replacement for black color</param>
        private void GetTextFromImage(string white, string black)
        {
            var textFromImage = new string[image.Height, image.Width];
            var pixels = GetPixels(image);
            for (int y = 0; y < image.Height; y++)
                for (int x = 0; x < image.Width; x++)
                {
                    if (pixels[y, x] == Color.FromArgb(255, Color.Black))
                        textFromImage[y, x] = black;
                    else 
                        textFromImage[y, x] = white;
                }
            textBox1.Text = WriteArray(textFromImage);
        }

        /// <summary>
        /// Starting text-to-image convertation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("The fields must be filled in", "ERROR", MessageBoxButtons.OK);
                return;
            }
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Load image please", "ERROR", MessageBoxButtons.OK);
                return;
            }
            whitePixel = Convert.ToString(textBox2.Text);
            blackPixel = Convert.ToString(textBox3.Text);
            GetTextFromImage(whitePixel, blackPixel);
        }
    }
}