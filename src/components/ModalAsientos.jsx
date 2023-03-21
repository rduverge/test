import React from "react";

const ModalAsientos = () => {
  return (
    <div className="modal">
      <div className="cerrar-modal">
        <img src={CerrarBtn} alt="cerrar modal" onClick={ocultarModal} />
      </div>
      <form
        className={`formulario ${animarModal ? "animar" : "cerrar"}`}
        action=""
      >
        <legend>Asientos contables</legend>

        <div className="campo">
          <label htmlFor="decripcionAsiento">Descripcion</label>
          <input
            type="text"
            placeholder="Añade una descripcion"
            id="decripcionAsiento"
          />
        </div>

        <div className="campo">
          <label htmlFor="idCliente">Cliente</label>
          <select id="idCliente">
            <option value="">-- Seleccione el cliente --</option>
          </select>
        </div>

        <div className="campo">
          <label htmlFor="cuenta">Cuenta</label>
          <input type="date" placeholder="Añade una cuenta" id="cuenta" />
        </div>

        <div className="campo">
          <label htmlFor="tipoMovimiento">Tipo de movimiento</label>
          <select id="tipoMovimiento">
            <option value="">-- Seleccione el tipo de movimiento --</option>
            <option value="Activo"> Debito </option>
            <option value="Inactivo"> Credito </option>
          </select>
        </div>

        <div className="campo">
          <label htmlFor="fechaAsiento">Fecha</label>
          <input
            type="date"
            placeholder="Añade la fecha del asiento"
            id="fechaAsiento"
          />
        </div>

        <div className="campo">
          <label htmlFor="montoAsiento">Monto</label>
          <input
            type="number"
            min="0"
            placeholder="Añade el monto del asiento"
            id="montoAsiento"
          />
        </div>

        <div className="campo">
          <label htmlFor="estadoAsiento">Estado</label>

          <select id="estadoAsiento">
            <option value="">-- Seleccione --</option>
            <option value="activo"> Activo </option>
            <option value="inactivo"> Inactivo </option>
          </select>
        </div>
        <input type="submit" value="Añadir transacción" />
      </form>
    </div>
  );
};

export default ModalAsientos;
