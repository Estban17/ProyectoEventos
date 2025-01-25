<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Eventos.MiPerfil" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mi Perfil</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script type="text/javascript">
        function validateFileUpload() {
            var file = document.getElementById('<%= FileUploadProfileImage.ClientID %>');
            var fileExtension = file.value.split('.').pop().toLowerCase();

            if (fileExtension != "jpg" && fileExtension != "png") {
                alert("Solo se permiten imágenes en formato JPG o PNG.");
                return false;
            }
            return true;
        }
    </script>
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

        <!-- Formulario para editar datos del perfil -->
        <div class="container mt-5">
            <h2 class="text-center mb-4">Mi Perfil</h2>
            <div class="row justify-content-center">
                <div class="text-center mb-4">
                    <asp:Button ID="BtnRegresar" runat="server" CssClass="btn btn-primary" Text="Regresar" OnClick="BtnRegresar_Click" />
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="FileUploadProfileImage">Subir Imagen de Perfil (JPG o PNG)</label>
                        <asp:FileUpload ID="FileUploadProfileImage" runat="server" CssClass="form-control" />
                        <asp:Label ID="LblImagen" runat="server" Text="No se ha seleccionado imagen." CssClass="text-muted"></asp:Label>
                    </div>

                    <!-- Vista previa de la imagen de perfil -->
                    <div class="form-group">
                        <label for="ImgPerfil">Vista previa de imagen de perfil</label>
                        <asp:Image ID="ImgPerfil" runat="server" CssClass="img-fluid" />
                    </div>

                    <div class="form-group">
                        <label for="TxtNombre">Nombre</label>
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="TxtPaterno">Apellido Paterno</label>
                        <asp:TextBox ID="TxtPaterno" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="TxtMaterno">Apellido Materno</label>
                        <asp:TextBox ID="TxtMaterno" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="TxtRFC">RFC</label>
                        <asp:TextBox ID="TxtRFC" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="TxtEmail">Correo Electrónico</label>
                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="TxtNumCelular">Número de Celular</label>
                        <asp:TextBox ID="TxtNumCelular" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group text-center mt-3">
                        <asp:Button ID="BtnActualizarPerfil" runat="server" Text="Actualizar Perfil" CssClass="btn btn-primary" OnClick="BtnActualizarPerfil_Click" OnClientClick="return validateFileUpload();" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
