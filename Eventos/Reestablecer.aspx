<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reestablecer.aspx.cs" Inherits="Eventos.Reestablecer" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <title>Reestablecer Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        body {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f8f9fa;
        }
        .card {
            width: 100%;
            max-width: 400px;
        }
        .btn-success {
            background-color: #900C3F;
            border-color: #900C3F;
        }
        .btn-success:hover {
            background-color: #581845;
            border-color: #900C3F;
        }
        .card-body {
            padding: 2rem;
        }
        .logo {
            max-height: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card shadow-sm">
            <div class="card-body">
                <!-- Logo del formulario -->
                <div class="text-center mb-4">
                    <img src="Imagen/images.png" alt="Logo" class="img-fluid logo">
                </div>
                <h3 class="card-title text-center mb-4">Reestablecer Contraseña</h3>

                <!-- Campos para nueva contraseña -->
                <asp:TextBox ID="TxtNuevaContrasena" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Nueva Contraseña"></asp:TextBox>
                <asp:TextBox ID="TxtConfirmarContrasena" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Confirmar Contraseña"></asp:TextBox>

                <!-- Botón para enviar -->
                <asp:Button ID="BtnReestablecer" runat="server" Text="Reestablecer Contraseña" CssClass="btn btn-success w-100" OnClick="BtnReestablecer_Click" />
            </div>
        </div>
    </form>
</body>
</html>
