<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerParticipantes.aspx.cs" Inherits="Eventos.VerParticipantes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Participantes del Evento</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        .container {
            max-width: 800px;
        }

        .table {
            margin-top: 20px;
        }

        .btn-primary {
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function imprimirTabla() {
            // Obtén el HTML de la tabla
            var tablaHTML = document.getElementById('<%= GvParticipantes.ClientID %>').outerHTML;

            // Crear una nueva ventana de impresión
            var ventanaImpresion = window.open('', '_blank', 'width=800, height=600');

            // Escribir el contenido de la tabla en la nueva ventana
            
            ventanaImpresion.document.write('<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">');
            ventanaImpresion.document.write('</head><body>');
            ventanaImpresion.document.write('<h2 class="text-center">Participantes del Evento</h2>');
            ventanaImpresion.document.write(tablaHTML);
            ventanaImpresion.document.write('</body></html>');

            // Esperar a que el contenido se cargue y luego ejecutar la impresión
            ventanaImpresion.document.close();
            ventanaImpresion.print();
        }
    </script>

</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <!-- Barra superior -->
        <nav class="navbar navbar-expand-lg navbar-dark" style="background-color: #800020;">
            <div class="container-fluid">
                <a class="navbar-brand" href="AdminEventos.aspx">Gestión de Eventos</a>
                <div class="d-flex">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-light btn-sm" PostBackUrl="Logout.aspx">Cerrar Sesión</asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- Botón Regresar fuera de la barra superior -->
        <div class="container">
            <p></p>
            <div class="text-center mb-4">
                <asp:Button ID="BtnRegresar" runat="server" CssClass="btn btn-primary" Text="Regresar" OnClick="BtnRegresar_Click" />
            </div>
            <!-- Botón de Imprimir -->
            <div class="text-center mb-4">
                <asp:Button ID="BtnImprimir" runat="server" CssClass="btn btn-success" Text="Imprimir" OnClientClick="imprimirTabla(); return false;" />
            </div>

            <h2 class="text-center mb-4">Participantes del Evento</h2>

            <!-- Tabla para mostrar participantes -->
            <asp:GridView ID="GvParticipantes" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" HeaderStyle-CssClass="table-dark" OnRowCommand="GvParticipantes_RowCommand" DataKeyNames="IdUsuario">
                <Columns>
                    <asp:BoundField DataField="IdUsuario" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                    <asp:BoundField DataField="NumCelular" HeaderText="Celular" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:TemplateField HeaderText="Confirmar Asistencia">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkConfirmar" runat="server" Checked='<%# Eval("AsistenciaConfirmada") != DBNull.Value && Convert.ToBoolean(Eval("AsistenciaConfirmada")) %>'
                                OnCheckedChanged="ChkConfirmar_CheckedChanged" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaConfirmacion" HeaderText="Fecha de Confirmación" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
