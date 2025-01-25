<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recuperar.aspx.cs" Inherits="Eventos.Recuperar" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <title>Recuperar Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        body {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #f8f9fa;
            margin: 0;
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
        .text-muted {
            font-size: 0.9em;
        }
        .card-body {
            padding: 2rem;
        }
        .footer-link {
            text-align: center;
            margin-top: 20px;
        }
        .footer-link a {
            text-decoration: none;
            color: #900C3F;
        }
        .footer-link a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="text-center mb-4">
                    <img src="Imagen/images.png" alt="Logo" class="img-fluid" style="max-height: 100px;">
                </div>
                <h3 class="card-title text-center mb-4">Recuperar Contraseña</h3>

                <p class="text-muted text-center mb-4">
                    Ingresa tu correo electrónico registrado para recibir un token que te permitirá restablecer tu contraseña.
                </p>

                <!-- Campo para correo electrónico -->
                <asp:TextBox ID="TxtCorreo" runat="server" CssClass="form-control mb-3" Placeholder="Correo electrónico"></asp:TextBox>

                <!-- Botón para enviar token -->
                <asp:Button ID="BtnRecuperar" runat="server" Text="Enviar Token" CssClass="btn btn-success w-100" OnClick="BtnRecuperar_Click" />

                <!-- Enlace para regresar al login -->
                <div class="footer-link">
                    <a href="Login.aspx">Volver a Iniciar Sesión</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
