import React, { useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

const ModalButton = ({ modalContent, title, btnName }) => {
  const [showModal, setShowModal] = useState(false);

  const handleCloseModal = () => {
    setShowModal(false);
  };

  const handleOpenModal = () => {
    setShowModal(true);
  };

  return (
    <div>
      <Button variant="btn btn-sm btn-outline-primary me-2" onClick={handleOpenModal}>
        {btnName}
      </Button>

      <Modal show={showModal} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>{title}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{modalContent}</Modal.Body>
        <Modal.Footer>
          
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default ModalButton;
