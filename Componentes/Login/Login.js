document.addEventListener('DOMContentLoaded', function() {
    // Selecciona el formulario de inicio de sesión
    const loginForm = document.getElementById('login-form');
  
    // Agrega un evento de escucha para el envío del formulario
    loginForm.addEventListener('submit', function(event) {
      event.preventDefault(); // Evita que el formulario se envíe por defecto
  
      // Obtiene los valores de usuario y contraseña
      const username = document.getElementById('username').value;
      const password = document.getElementById('password').value;
  
      // Aquí puedes agregar la lógica para validar las credenciales y realizar el inicio de sesión
      // Por ahora, solo mostraremos las credenciales en la consola
      console.log('Usuario:', username);
      console.log('Contraseña:', password);

      window.location.href = '../Menu/Menu.html';
    });
  });
  