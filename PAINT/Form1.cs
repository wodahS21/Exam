using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PAINT
{
    public partial class Form1 : Form
    {
        public Graphics lienzo;
        Graphics g;
        Graphics papel;
        int x = 0;
        int y = 0;
        int R = 0;
        int G = 0;
        int B = 0;
        int tamanioPincel = 3;
        bool moviendo = false;
        Pen pen;
        bool pintar = false;
        bool borrar = false;
        SolidBrush relleno = new SolidBrush(Color.Black);
        Bitmap bm;
        Point py, px;
        int ix, iy, sX, sY, cX, cY;
        Pen borrador = new Pen(Color.White, 10);

        public Form1()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 700;
            bm = new Bitmap(txtPapel.Width, txtPapel.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);

            txtPapel.Image = bm;

            txtPapel.Image = new Bitmap(txtPapel.Height, txtPapel.Width);
            papel = txtPapel.CreateGraphics();
            papel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            txtPapel.Image = bm;
            R = 0;
            G = 0;
            B = 0;
            pen = new Pen(Color.FromArgb(R, G, B), tamanioPincel);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void txtPapel_MouseMove(object sender, MouseEventArgs e)
        {
            if (moviendo && pintar)
            {
                cambiarPincel(0, 0, 0);
                //DIBUJAMOS UNA LINEA EN LA POSICION ACRRUAL HACIA DONDE NOS MOVEMOS
                papel.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
                px = e.Location;
                g.DrawLine(pen, px, py);
                py = px;

                
                txtPapel.Refresh();
                //SI EL MOUSE SE ESTA MOVIENDO EMPEZARA Y TERMINARA EL PUNTO SEGUN ALTURA Y ANCHURA XY
                ix = e.X;
                iy = e.Y;
                sX = e.X - cX;
                sY = e.Y - cY;

            }
            if (moviendo && borrar)
            {
                cambiarPincel(255, 255, 255);
                papel.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;

            }

        }
    
        private void txtPapel_MouseDown(object sender, MouseEventArgs e)
        {
            moviendo = true;

            x = e.X;
            y = e.Y;
        txtPapel.Cursor = Cursors.Cross;
        py = e.Location;
            //PARA SI EL MOUSE BAJA, DIBUJARA EN LAS CORDENADAS MARCADAS DE X Y 
            cX = e.X;
            cY = e.Y;


    }
        //SE DISPARA EL DEJAR DE HACER CLICK SOSTENIDO SOBRE EL PICTUREBOX
        private void txtPapel_MouseUp(object sender, MouseEventArgs e)
        {
            moviendo = false;
            sX= ix - cX;
            sY= iy - cY;
           
            

        }
        

        private void btnPintar_Click(object sender, EventArgs e)
        {
            pintar = true;
            borrar = false;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            pintar = false;
            borrar = true;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pen = new Pen(Color.Black);
            relleno = new SolidBrush(Color.Black);
           
        }


        

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen = new Pen(colorDialog1.Color);
                relleno = new SolidBrush(colorDialog1.Color);
            }
        }

        private void cambiarPincel(int R, int G, int B)
        {
            pen = new Pen(Color.FromArgb(R, G, B), 3);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
         
        private void btnGuardar_Click(object sender, EventArgs e)
        {
         
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
   

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, txtPapel.Width, txtPapel.Height), bm.PixelFormat);
               btm.Save(guardar.FileName, ImageFormat.Png);
                 
               
                }
            

            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {

            OpenFileDialog getimage = new OpenFileDialog();
            DialogResult result = getimage.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPapel.Image = Image.FromFile(getimage.FileName);

            }

        }

        private void btnBorra_Click(object sender, EventArgs e)
        {
            Graphics papel = txtPapel.CreateGraphics();
            papel.Clear(Color.White);
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lienzo = CreateGraphics();

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            var guardar = new SaveFileDialog();
            guardar.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";


            if (guardar.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, txtPapel.Width, txtPapel.Height), bm.PixelFormat);
                btm.Save(guardar.FileName, ImageFormat.Jpeg);


            }
        }

        private void txtPapel_Paint(object sender, PaintEventArgs e)
        {
            Graphics papel = e.Graphics;

        }



    }
}