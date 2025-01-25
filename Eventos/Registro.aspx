<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Eventos.Registro" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <title>Registro de Usuario</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="text-center mb-4">
                    <img src="Imagen/images.png" alt="Logo" class="img-fluid" style="max-height: 100px;">
                </div>
                <h3 class="card-title text-center mb-4">Registro de Usuario</h3>

                <!-- Campos de formulario -->
                <div class="mb-3">
                    <label for="TxtNombre" class="form-label">Nombre</label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtPaterno" class="form-label">Apellido Paterno</label>
                    <asp:TextBox ID="TxtPaterno" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtMaterno" class="form-label">Apellido Materno</label>
                    <asp:TextBox ID="TxtMaterno" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtRFC" class="form-label">RFC</label>
                    <asp:TextBox ID="TxtRFC" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtEmail" class="form-label">Correo Electrónico</label>
                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                </div>
                <div class="mb-3">
                    <label for="TxtNumCelular" class="form-label">Numero Celular</label>
                    <asp:TextBox ID="TxtNumCelular" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <!-- Botón de registro -->
                <asp:Button ID="BtnRegistro" runat="server" Text="Registrarse" CssClass="btn btn-success w-100" OnClick="BtnRegistro_Click" />

                <!-- Enlace a la página de inicio de sesión -->
                <div class="footer-link">
                    <span class="text-muted">¿Ya tienes cuenta?</span>
                    <a href="Login.aspx">Inicia sesión aquí</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
