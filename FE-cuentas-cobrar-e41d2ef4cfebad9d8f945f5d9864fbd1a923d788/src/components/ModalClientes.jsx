import React from "react";

const ModalClientes = () => {
  return (
    <div className="modal">
      <div className="cerrar-modal">
        <img src={CerrarBtn} alt="cerrar modal" onClick={ocultarModal} />
      </div>
      <form
        className={`formulario ${animarModal ? "animar" : "cerrar"}`}
        action=""
      >
        <legend>Clientes</legend>

        <div className="campo">
          <label htmlFor="nombre">Nombre del cliente</label>
          <input
            type="text"
            placeholder="Añade el nombre del cliente"
            id="nombre"
          />
        </div>

        <div className="campo">
          <label htmlFor="cedula">Cédula del cliente</label>
          <input
            type="text"
            maxlength="13"
            placeholder="Añade la cédula del cliente"
            id="cedula"
          />
        </div>

        <div className="campo">
          <label htmlFor="limiteCredito">Límite de crédito</label>
          <input
            type="number"
            min="0"
            placeholder="Añade el límite de crédito"
            id="limiteCredito"
          />
        </div>

        <div className="campo">
          <label htmlFor="estadoCliente">Estado</label>
          <select id="estadoCliente">
            <option value="">-- Seleccione el estado --</option>
            <option value="Activo"> Activo </option>
            <option value="Inactivo"> Inactivo </option>
          </select>
        </div>
      </form>
    </div>
  );
};

export default ModalClientes;
