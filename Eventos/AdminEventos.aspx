<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEventos.aspx.cs" Inherits="Eventos.AdminEventos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrar Eventos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        .card {
            border: none;
            border-radius: 10px;
            overflow: hidden;
            transition: transform 0.2s, box-shadow 0.2s;
        }

            .card:hover {
                transform: scale(1.02);
                box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
            }

        .card-img-top {
            border-bottom: 2px solid #800020;
        }

        .btn-primary {
            background-color: #800020;
            border-color: #800020;
        }

            .btn-primary:hover {
                background-color: #a0002e;
                border-color: #a0002e;
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
                    <asp:LinkButton ID="BtnLogout" runat="server" CssClass="btn btn-outline-light btn-sm" PostBackUrl="Logout.aspx">
                        Cerrar Sesión
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- Contenido principal -->
        <div class="container mt-5">
            <h2 class="text-center mb-4">Gestión de Eventos</h2>
            <div class="text-center mb-4">
                <asp:Button ID="BtnNuevoEvento" runat="server" CssClass="btn btn-primary" Text="Crear Nuevo Evento" OnClick="BtnNuevoEvento_Click" />
            </div>

            <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
                <asp:Repeater ID="RptEventos" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card shadow-sm">
                                <img src='<%# Eval("Imagen") %>' class="card-img-top" alt="Evento" style="height: 200px; object-fit: cover;">
                                <div class="card-body">
                                    <h5 class="card-title text-truncate"><%# Eval("Nombre") %></h5>
                                    <p class="card-text text-muted" style="max-height: 60px; overflow: hidden; text-overflow: ellipsis;"><%# Eval("Descripcion") %></p>
                                    <p><strong>Lugar:</strong> <%# Eval("Lugar") %></p>
                                    <p><strong>Fecha:</strong> <%# Eval("FechaInicio", "{0:dd/MM/yyyy HH:mm}") %></p>
                                    <div class="d-flex justify-content-between mt-3">
                                        <asp:Button ID="BtnEditar" runat="server" Text="Editar" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IdEvento") %>' OnCommand="BtnEditar_Click" />
                                        <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IdEvento") %>' OnCommand="BtnEliminar_Click" />
                                        <asp:Button ID="BtnVerParticipantes" runat="server" Text="Participantes" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("IdEvento") %>' OnCommand="BtnVerParticipantes_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
