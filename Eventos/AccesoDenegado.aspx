<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="Eventos.AccesoDenegado" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Acceso Denegado</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
    <style>
        body {
            background-color: #f8f9fa;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .access-denied-container {
            background-color: #ffffff;
            padding: 3rem;
            border-radius: 1rem;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        .access-denied-container h1 {
            color: #dc3545;
            font-size: 3rem;
        }
        .access-denied-container p {
            font-size: 1.2rem;
            color: #495057;
        }
        .btn-custom {
            background-color: #900C3F;
            border-color: #900C3F;
        }
        .btn-custom:hover {
            background-color: #581845;
            border-color: #900C3F;
        }
    </style>
</head>
<body>
    <div class="access-denied-container">
        <h1>Acceso Denegado</h1>
        <p>No tienes permiso para acceder a esta página. Si crees que esto es un error, por favor contacta al administrador.</p>
        <a href="Login.aspx" class="btn btn-custom mt-4 px-5">Volver al Login</a>
    </div>
</body>
</html>
