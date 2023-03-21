import React from "react";
import CerrarBtn from "../img/cerrar.svg";

const Modal = ({ setModal, animarModal, setAnimarModal }) => {
  const ocultarModal = () => {
    setAnimarModal(false);

    setTimeout(() => {
      setModal(false);
    }, 500);
  };
  return (
    <div className="modal">
      <div className="cerrar-modal">
        <img src={CerrarBtn} alt="cerrar modal" onClick={ocultarModal} />
      </div>
      <form
        className={`formulario ${animarModal ? "animar" : "cerrar"}`}
        action=""
      >
        <legend>Tipos documentos</legend>

        <div className="campo">
          <label htmlFor="decripcionTipoDocumento">Descripcion</label>
          <input
            type="text"
            placeholder="Añade una descripcion"
            id="decripcionTipoDocumento"
          />
        </div>

        <div className="campo">
          <label htmlFor="cuentaContable">Cuenta contable</label>
          <input
            type="text"
            placeholder="Añade una cuenta contable"
            id="cuentaContable"
          />
        </div>

        <div className="campo">
          <label htmlFor="estadoTipoDocumento">Estado</label>
          <select id="estadoTipoDocumento">
            <option value="">-- Seleccione --</option>
            <option value="activo"> Activo </option>
            <option value="inactivo"> Inactivo </option>
          </select>
        </div>
        <input type="submit" value="Añadir tipo de documento" />
      </form>
    </div>
  );
};

export default Modal;
