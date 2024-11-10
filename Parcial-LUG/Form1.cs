using BLL;
using DAL;
using Entity;

namespace Parcial_LUG
{
    public partial class Form1 : Form
    {

        private PartidoNegocio partidoNegocio = new PartidoNegocio();
        public Form1()
        {
            InitializeComponent();
            CargarDeportes();
            CargarPartidos();
        }

        private void CargarDeportes()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = partidoNegocio.ObtenerDeportes();     
            comboBox1.ValueMember = "IdDeporte";
            comboBox1.DisplayMember = "Descripcion";            
        }

        private void CargarPartidos()
        {
            List<Partido> partidos = partidoNegocio.ObtenerPartidos();
            dataGridView1.DataSource = partidos;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Partido partido = new Partido
                {
                    IdDeporte = comboBox1.SelectedIndex + 1,
                    EquipoLocal = textBox1.Text,
                    EquipoVisitante = textBox2.Text,
                    FechaPartido = dateTimePicker1.Value
                };
                partidoNegocio.CrearPartido(partido);
                CargarPartidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idPartido = int.Parse(textBox3.Text);
            partidoNegocio.EliminarPartido(idPartido);
            CargarPartidos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idPartido = int.Parse(textBox4.Text);
            int marcadorLocal = int.Parse(textBox6.Text);
            int marcadorVisitante = int.Parse(textBox5.Text);
            try
            {
                partidoNegocio.ActualizarMarcador(idPartido, marcadorLocal, marcadorVisitante);
                CargarPartidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
