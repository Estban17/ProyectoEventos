<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Eventos.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <title>Iniciar Sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
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
        .g-recaptcha {
            margin-bottom: 15px;
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
        .forgot-password {
            margin-top: 10px;
            text-align: center;
        }
        .forgot-password a {
            text-decoration: none;
            color: #900C3F;
        }
        .forgot-password a:hover {
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
                <h3 class="card-title text-center mb-4">Iniciar Sesión</h3>

                <!-- Campos de formulario -->
                <asp:TextBox ID="TxtRFC" runat="server" CssClass="form-control mb-3" Placeholder="RFC"></asp:TextBox>
                <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>

                <!-- Google reCAPTCHA -->
                <div class="g-recaptcha" data-sitekey="6Le5QLMqAAAAAKxM72xI8oM5tu3uYS6lxRw92m0G"></div>

                <!-- Botón de login -->
                <asp:Button ID="BtnLogin" runat="server" Text="Ingresar" CssClass="btn btn-success w-100" OnClick="BtnLogin_Click" />

                <!-- Enlace de recuperación de contraseña -->
                <div class="forgot-password">
                    <a href="Recuperar.aspx">¿Olvidaste tu contraseña?</a>
                </div>

                <!-- Enlace de registro -->
                <div class="footer-link">
                    <span class="text-muted">¿Aún no estás registrado?</span>
                    <a href="Registro.aspx">Regístrate aquí</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
