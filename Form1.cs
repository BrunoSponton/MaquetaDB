using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ConexionDB;

namespace DBConcesionaria
{
    public partial class Form1 : Form
    {
        private SQL conexionDB;

        public Form1()
        {
            InitializeComponent();
            conexionDB = new SQL();
            LlenarDataGridView("VENDEDORES", dataGridViewVendedores);
            LlenarDataGridView("CLIENTES", dataGridViewClientes);
            LlenarDataGridView("PLANES", dataGridViewPlanes);
            LlenarDataGridView("SUSCRIPCIONES", dataGridViewSuscripciones);
            LlenarDataGridView("ADJUDICACIONES", dataGridViewAdjudicaciones);
            LlenarDataGridView("PAGOS", dataGridViewPagos);
        }

        private void LlenarDataGridView(string tabla, DataGridView dataGridView)
        {
            try
            {
                conexionDB.Abrirconexion();

                string query = $"SELECT * FROM {tabla}";
                SqlCommand command = new SqlCommand(query, conexionDB.conexion);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView.DataSource = dataTable;

                // Ajustar el ancho de las columnas según el contenido
                AjustarAnchoColumnas(dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos de la tabla {tabla}: {ex.Message}");
            }
            finally
            {
                conexionDB.Cerrarconexion();
            }
        }

        private void AjustarAnchoColumnas(DataGridView dataGridView)
        {
            // Ajustar el ancho de todas las columnas según el contenido
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Opcional: Ajustar el modo de ajuste automático de tamaño de columnas
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}
