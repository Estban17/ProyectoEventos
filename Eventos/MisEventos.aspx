<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MisEventos.aspx.cs" Inherits="Eventos.MisEventos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mis Eventos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
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
        .bg-light {
            background-color: #f8f9fa !important;
        }
        .card-body p {
            margin-bottom: 0.5rem;
        }
        .event-title {
            font-weight: bold;
            color: #800020;
        }
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <!-- Barra superior -->
        <nav class="navbar navbar-expand-lg navbar-dark" style="background-color: #800020;">
            <div class="container-fluid">
                <a class="navbar-brand" href="EventosParticipante.aspx">Eventos Disponibles</a>
                <div class="d-flex">
                    <asp:LinkButton ID="BtnLogout" runat="server" CssClass="btn btn-outline-light btn-sm" PostBackUrl="Logout.aspx">
                        Cerrar Sesión
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- Contenido principal -->
        <div class="container mt-5">
            <h2 class="text-center mb-4">Mis Eventos</h2>
            <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
                <asp:Repeater ID="RepeaterMisEventos" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card shadow-sm">
                                <img src='<%# Eval("Imagen") %>' class="card-img-top" alt="Imagen del evento" style="height: 200px; object-fit: cover;">
                                <div class="card-body">
                                    <h5 class="event-title text-truncate"><%# Eval("Nombre") %></h5>
                                    <p class="card-text text-muted" style="max-height: 60px; overflow: hidden; text-overflow: ellipsis;"><%# Eval("Descripcion") %></p>
                                    <p><strong>Lugar:</strong> <%# Eval("Lugar") %></p>
                                    <p><strong>Fecha:</strong> <%# Eval("FechaInicio", "{0:dd/MM/yyyy HH:mm}") %> - <%# Eval("FechaFin", "{0:dd/MM/yyyy HH:mm}") %></p>
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
