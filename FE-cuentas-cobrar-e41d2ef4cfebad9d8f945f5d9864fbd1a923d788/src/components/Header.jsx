import React from "react";
import TipoDocumento from "./TipoDocumento";

const Header = () => {
  return (
    <header>
      <h1>Cuentas por Cobrar</h1>
      <ul id="Navbar">

          <li id="Navitem">
            <a href="#">Tipos de documento</a>
          </li>
          <li id="Navitem">
            <a href="">Clientes</a>
          </li>
          <li id="Navitem">
            <Link to="">Transacciones</Link>
          </li>
          <li id="Navitem">
            <a href="#">Asientos contables</a>
          </li>

      </ul>
    </header>
  );
};

export default Header;
