<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearEvento.aspx.cs" Inherits="Eventos.CrearEvento" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crear Nuevo Evento</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        .container {
            max-width: 800px;
        }

        .card {
            margin-top: 20px;
        }

        .form-label {
            font-weight: bold;
        }

        .btn-primary {
            width: 100%;
        }

    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <!-- Barra superior -->
        <nav class="navbar navbar-expand-lg navbar-dark" style="background-color: #800020;">
            <div class="container-fluid">
                <a class="navbar-brand" href="AdminEventos.aspx">Gestión de Eventos</a>
                <div class="d-flex">
                    <asp:LinkButton ID="BtnLogout" runat="server" CssClass="btn btn-outline-light btn-sm" PostBackUrl="Logout.aspx">Cerrar Sesión</asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- Botón Regresar fuera de la barra superior -->
        <div class="container">
            <p></p>
            <div class="text-center mb-4">
                <asp:Button ID="BtnRegresar" runat="server" CssClass="btn btn-primary" Text="Regresar" OnClick="BtnRegresar_Click" />
            </div>

            <!-- Formulario de Crear Evento -->
            <div class="card shadow">
                <div class="card-body">
                    <h3 class="card-title text-center mb-4">Crear Nuevo Evento</h3>
                    <div class="mb-3">
                        <label for="TxtNombre" class="form-label">Nombre del Evento</label>
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="TxtDescripcion" class="form-label">Descripción</label>
                        <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="mb-3">
                        <label for="TxtImagen" class="form-label">URL de Imagen</label>
                        <asp:TextBox ID="TxtImagen" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="DdlTipoEvento" class="form-label">Tipo de Evento</label>
                        <asp:DropDownList ID="DdlTipoEvento" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Aire Libre">Aire Libre</asp:ListItem>
                            <asp:ListItem Value="Auditorio">Auditorio</asp:ListItem>
                            <asp:ListItem Value="Sala">Sala</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="DdlEstadoEvento" class="form-label">Estado de Evento</label>
                        <asp:DropDownList ID="DdlEstadoEvento" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Activo">Activo</asp:ListItem>
                            <asp:ListItem Value="Suspendido">Suspendido</asp:ListItem>
                            <asp:ListItem Value="Cancelado">Cancelado</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="TxtLugar" class="form-label">Lugar</label>
                        <asp:TextBox ID="TxtLugar" runat="server" CssClass="form-control" />
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label for="TxtFechaInicio" class="form-label">Fecha de Inicio</label>
                            <input type="date" id="TxtFechaInicio" runat="server" class="form-control" />
                            <label for="TxtHoraInicio" class="form-label mt-2">Hora de Inicio</label>
                            <input type="time" id="TxtHoraInicio" runat="server" class="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label for="TxtFechaFin" class="form-label">Fecha de Fin</label>
                            <input type="date" id="TxtFechaFin" runat="server" class="form-control" />
                            <label for="TxtHoraFin" class="form-label mt-2">Hora de Fin</label>
                            <input type="time" id="TxtHoraFin" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="mb-3">
                        <label for="TxtRangoEdad" class="form-label">Rango de Edad (Ejemplo: 18-35)</label>
                        <asp:TextBox ID="TxtRangoEdad" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label for="DdlGenero" class="form-label">Género de los Asistentes</label>
                        <asp:DropDownList ID="DdlGenero" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Todos">Todos</asp:ListItem>
                            <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                            <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar Evento" CssClass="btn btn-primary" OnClick="BtnGuardar_Click" />
                </div>
            </div>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>
</body>
</html>