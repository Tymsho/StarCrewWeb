# 🚀 Star Crew Web Manager

**Star Crew Web Manager** es la migración del gestor de tripulación original, adaptado para la web utilizando **ASP.NET Web Forms** y **C#**.

La aplicación mantiene la arquitectura original de **tres capas (Modelo-Controlador-Vista)**, donde la capa de negocio y acceso a datos se ha reutilizado de forma íntegra. El proyecto permite gestionar tripulantes, asignarlos a misiones y simular sus resultados, ahora accesibles desde cualquier navegador.

---

## 🎨 Temática y Diseño

El diseño de la aplicación ha sido actualizado con una estética **Retro-Futurista tipo terminal** (verde sobre negro) inspirada en los sistemas operativos de películas de ciencia ficción como Alien (Weyland-Yutani), logrando una interfaz funcional y utilitaria.

---

## ⚙️ Configuración y Ejecución (Web)

**Requisitos previos:**
* Visual Studio con carga de trabajo **Desarrollo web y ASP.NET**.
* Instancia de **SQL Server** (Express o LocalDB).

### Pasos para configurar el entorno:

1.  **Base de Datos:** Asegúrate de que la base de datos de la aplicación esté creada en SQL Server (usando el script original `query.txt`).
2.  **Cadena de Conexión (Web.config):**
    * Abre el archivo **Web.config** en la raíz del proyecto.
    * Busca la sección `<connectionStrings>`.
    * Modifica la entrada `StarCrewDB` para que apunte correctamente a tu instancia de SQL Server.

    ```xml
    <connectionStrings>
        <add name="StarCrewDB" 
             connectionString="Server=TU_SERVIDOR;Database=StarCrew;Trusted_Connection=True;" 
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```
3.  **Compilación y Enlaces:** Verifica que los proyectos **Modelo** y **Controlador** estén correctamente referenciados por el proyecto **StarCrewWeb** (Revisa las **Referencias** en el Explorador de Soluciones).
4.  **Ejecutar:** Presiona **F5**. La aplicación se abrirá en el navegador por defecto.

---

## 🧭 Navegación y Vistas (ASPX)

La navegación sigue el flujo del patrón **Master Page** (`Site.Master`), que contiene el menú superior y el diseño de terminal.

* **Default.aspx (Inicio):** Página de presentación con el fondo de pantalla de la terminal.
* **Tripulantes.aspx:** Gestión de alta, edición y eliminación de personal.
* **Misiones.aspx:** Asignación de tripulantes disponibles a misiones activas.
* **Historial.aspx:** Revisión y finalización de misiones activas.

---

## 👨‍🚀 Funcionalidades Clave

### 1. Gestión de Tripulantes (`Tripulantes.aspx`)
* Se listan los tripulantes en un **Repeater** personalizable.
* Los botones **Editar** y **Eliminar** en cada fila utilizan el evento **RowCommand** para interactuar con la lógica de negocio.
* El modo de edición se maneja utilizando el **ViewState** para mantener el `TripulanteId` activo entre PostBacks.

### 2. Asignación de Misiones (`Misiones.aspx`)
* La selección de misión utiliza un **DropDownList** con **AutoPostBack** para mostrar dinámicamente los **Requisitos** y la **Dificultad** en tiempo real.
* La selección de personal disponible se realiza mediante un **ListBox** de selección múltiple.
* La lógica de la capa de **Controlador** determina qué tripulantes están "Pendientes" de una misión y los excluye de la lista de disponibles.

### 3. Finalización y Resultados (`Historial.aspx`)
* Se utiliza un **DropDownList** con las misiones que tienen asignaciones con estado **"Pendiente"**.
* Al hacer clic en **Finalizar Misión**, se llama al controlador, que valida los requisitos (Roles, NivelHabilidad).
* El resultado (**Exitosa** o **Fallida**) y la subida de nivel de habilidad de los tripulantes se gestiona íntegramente en la capa de **Controlador**, demostrando la **separación de responsabilidades**.
* El historial de misiones finalizadas se lista en un **GridView**.

---

### 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#
- **Framework:** .NET Framework (ASP.NET Web Forms)
- **Base de Datos:** SQL Server
- **Arquitectura:** Patrón en capas (Modelo, Controlador, Vista)
- **Control de Estado:** ViewState y Session (mínimo)
- **Estilo:** CSS 3 (Temática Terminal Retro)
