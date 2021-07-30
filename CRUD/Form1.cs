﻿using MySql.Data.MySqlClient;
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
        public string codigoCifrado = "";
        public string codigoDescifrado = "";
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Cifrado
        public string cifrarCodigo()
        {
            char[] numeric =
            {
                ' ',/*'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z',*/ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            string conversor = "-,A,B,D,G,K,O,U,C,L,S";
            string[] conversorSplit = conversor.Split(',');
            char[] codigoPlano = txtCodigo.Text.ToCharArray();

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
        #endregion

        #region Descifrado
        public string descifrarCodigo()
        {
            char[] alfa =
            {
                ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
            string conversor = "-,A,B,D,G,K,O,U,C,L,S";
            string[] conversorSplit = conversor.Split(',');
            string[] codigoDes = txtCodigo.Text.Split(' ');

            for (int i = 0; i < codigoDes.Length; i++)
            {
                for (int j = 0; j < conversorSplit.Length; j++)
                {
                    if (conversorSplit[j] == codigoDes[i])
                    {
                        codigoDescifrado = codigoDescifrado + alfa[j];
                    }
                }
            }
            return codigoDescifrado;
        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                cifrarCodigo();
                txtCodigo.Text = codigoCifrado;
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
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                } else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            } catch(FormatException fex)
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
                } else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            } catch(MySqlException ex)
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
            String id = txtId.Text;
            String codigo = txtCodigo.Text;
            String nombre = txtNombre.Text;
            String descripcion = txtDescripcion.Text;
            double precio_publico = double.Parse(txtPrecioPublico.Text);
            int existencias = int.Parse(txtExistencias.Text);

            string sql = "UPDATE productos SET codigo='"+codigo+"', nombre='"+nombre+ "', descripcion='" + descripcion + "', precio_publico='" + precio_publico + "', existencias='" + existencias + "' WHERE id='"+id+"'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro modificado");
                limpiar();
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
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";
            txtCodigo.Enabled = true;
        }

        private void txtRealCodigo_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtRealCodigo_Click(object sender, EventArgs e)
        {   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            descifrarCodigo();
            txtRealCodigo.Text = codigoDescifrado;
        }
    }
}