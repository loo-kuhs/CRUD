using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        #region Variables
        public string codigoCifrado = string.Empty;
        public string codigoDescifrado = string.Empty;
        public string textoCifrado = string.Empty;
        public string textoDescifrado = string.Empty;
        #endregion

        public Form1()
        {
            InitializeComponent();
            ToolTip tipParaTontos = new ToolTip();

            //tipForDummie.AutoPopDelay = 400;
            //tipForDummie.InitialDelay = 1000;
            //tipForDummie.ReshowDelay = 500;

            this.tipForDummie.ShowAlways = true;

            this.tipForDummie.SetToolTip(this.txtCodigo, "Solo numeros y un espacio entre ellos.");
            this.tipForDummie.SetToolTip(this.txtNombre, "Solo numeros, letras minusculas y mayusculas");
            this.tipForDummie.SetToolTip(this.txtDescripcion, "Solo numeros, letras minusculas y mayusculas");
            txtRealCodigo.Enabled = false;
        }

        #region Cifrado
        public string cifrarCodigo(string txtBox)
        {
            char[] numeric =
            {
                ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            string conversor = "-,A,B,D,G,K,O,U,C,L,T";
            string[] conversorSplit = conversor.Split(',');
            char[] codigoPlano = txtBox.ToCharArray();

            for (int i = 0; i < codigoPlano.Length; i++)
            {
                for (int j = 0; j < numeric.Length; j++)
                {
                    if (numeric[j] == codigoPlano[i])
                    {
                        codigoCifrado = codigoCifrado + conversorSplit[j];
                    }
                }
            }
            return codigoCifrado;
        }

        public string cifrarNomAndDesc(string textBox)
        {
            char[] alfanumerico =
            {
                ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 
                'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 
                'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 
                'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 
                'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', 
                '5', '6', '7', '8', '9'
            };
            string ToSymb = "%,m97,m98,m99,m100,m101,m102,m103,m104,m105,m106,m107,m108,m109,m110,m111,m112,m113,m114,m115,m116,m117,m118,m119,m120,m121,m122,m123,M65,M66,M67,M68,M69,M70,M71,M72,M73,M74,M75,M76,M77,M78,M79,M80,M81,M82,M83,M84,M85,M86,M87,M89,M90,M91,M92,n48,n49,n50,n51,n52,n53,n54,n55,n56,n57";
            string[] conversorSplit = ToSymb.Split(',');
            char[] textoPlano = textBox.ToCharArray();

            for (int i = 0; i < textoPlano.Length; i++)
            {
                for (int j = 0; j < alfanumerico.Length; j++)
                {
                    if (alfanumerico[j] == textoPlano[i])
                    {
                        textoCifrado = textoCifrado + conversorSplit[j] + " ";
                    }
                }
            }
            return textoCifrado;
        }
        #endregion

        #region Descifrado
        public string descifrarCodigo(string codigo)
        {
            char[] alfa =
            {
                '-', 'A', 'B', 'D', 'G', 'K', 'O', 'U', 'C', 'L', 'T'
            };
            string conversor = " ,0,1,2,3,4,5,6,7,8,9";
            string[] conversorSplit = conversor.Split(',');
            char[] codigoDes = codigo.ToCharArray();

            for (int i = 0; i < codigoDes.Length; i++)
            {
                for (int j = 0; j < alfa.Length; j++)
                {
                    if (alfa[j] == codigoDes[i])
                    {
                        codigoDescifrado = codigoDescifrado + conversorSplit[j];
                    }
                }
            }
            return codigoDescifrado;

        }

        public string descifrarNomAndDesc(string textBox)
        {
           
            char[] alfanumerico =
            {
                ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
                'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
                'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S',
                'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4',
                '5', '6', '7', '8', '9'
            };
            string ToSymb = "%,m97,m98,m99,m100,m101,m102,m103,m104,m105,m106,m107,m108,m109,m110,m111,m112,m113,m114,m115,m116,m117,m118,m119,m120,m121,m122,m123,M65,M66,M67,M68,M69,M70,M71,M72,M73,M74,M75,M76,M77,M78,M79,M80,M81,M82,M83,M84,M85,M86,M87,M89,M90,M91,M92,n48,n49,n50,n51,n52,n53,n54,n55,n56,n57";
            string[] conversorSplit = ToSymb.Split(',');
            string[] textoPlano = textBox.Split(' ');

            for (int i = 0; i < textoPlano.Length; i++)
            {
                for (int j = 0; j < conversorSplit.Length; j++)
                {
                    if (conversorSplit[j] == textoPlano[i])
                    {
                        textoDescifrado = textoDescifrado + alfanumerico[j];
                    }
                }
            }

            return textoDescifrado;
        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                cifrarCodigo(txtCodigo.Text);
                txtCodigo.Text = codigoCifrado;
                cifrarNomAndDesc(txtDescripcion.Text);
                txtDescripcion.Text = textoCifrado;

                String codigo = txtCodigo.Text;
                String nombre = txtNombre.Text;
                String descripcion = txtDescripcion.Text;
                double precio_publico = double.Parse(txtPrecioPublico.Text);
                int existencias = int.Parse(txtExistencias.Text);

                if (codigo != "" && nombre != "" && descripcion != "" && precio_publico > 0 && existencias > 0)
                {

                    string sql = "INSERT INTO productos (codigo, nombre, descripcion, precio_publico, existencias) VALUES ('" + codigo + "', '" + nombre + "','" + descripcion + "','" + precio_publico + "','" + existencias + "')";

                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro guardado");
                        limpiar();
                        Application.Restart();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String codigo = txtCodigo.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias FROM productos WHERE codigo LIKE '" + codigo + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtCodigo.Text = reader.GetString(1);
                        txtCodigo.Enabled = false;
                        txtNombre.Text = reader.GetString(2);
                        txtDescripcion.Text = reader.GetString(3);
                        txtPrecioPublico.Text = reader.GetString(4);
                        txtExistencias.Text = reader.GetString(5);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cifrarNomAndDesc(txtDescripcion.Text);
            txtDescripcion.Text = textoCifrado;

            String id = txtId.Text;
            String codigo = txtCodigo.Text;
            String nombre = txtNombre.Text;
            String descripcion = txtDescripcion.Text;
            double precio_publico = double.Parse(txtPrecioPublico.Text);
            int existencias = int.Parse(txtExistencias.Text);

            string sql = "UPDATE productos SET codigo='" + codigo + "', nombre='" + nombre + "', descripcion='" + descripcion + "', precio_publico='" + precio_publico + "', existencias='" + existencias + "' WHERE id='" + id + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro modificado");
                limpiar();
                Application.Restart();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String id = txtId.Text;

            string sql = "DELETE FROM productos WHERE id='" + id + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado");
                limpiar();
                Application.Restart();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Clear();
            txtCodigo.ResetText();
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";
            txtRealCodigo.Clear();
            txtRealCodigo.ResetText();
            txtRealCodigo.Refresh();
            txtCodigo.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            descifrarCodigo(txtCodigo.Text);
            txtRealCodigo.Text = codigoDescifrado;
            descifrarNomAndDesc(txtDescripcion.Text);
            txtDescripcion.Text = textoDescifrado;

            //MessageBox.Show(textoDescifrado);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsUpper(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '-')
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            //if ((e.KeyChar == ' ') && (sender as TextBox).Text.IndexOf(" ") > -3)
            //{
            //    e.Handled = true;
            //}
        }

        private void txtPrecioPublico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf(".") > -1)
            {
                e.Handled = true;
            }
        }

        private void txtExistencias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
