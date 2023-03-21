import { useState } from 'react'
import Header from './components/Header'
import TipoDocumento from './components/TipoDocumento'
import Modal from './components/Modal'
import IconoNuevo from './img/nuevo-gasto.svg'

function App() {
  const[modal, setModal] = useState(false);
  const[animarModal, setAnimarModal] = useState(false);

  const handleNuevoGasto = () =>{
    setModal("true")
    
    setTimeout(() =>{
      setAnimarModal(true)
    }, 500);
  }

  return (
      <div> 
        <Header />
        <TipoDocumento />
        <div className="nuevo-gasto">
          <img src={IconoNuevo} alt="iconnew" onClick={handleNuevoGasto} />
        </div>
        {modal && <Modal setModal= {setModal} animarModal={animarModal} setAnimarModal={setAnimarModal} />}
      </div>

  )
}

export default App
