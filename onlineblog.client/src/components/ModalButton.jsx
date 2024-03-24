import React, { useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

const ModalComponent = ({ btnName, modalContent, title }) => {
    const [showModal, setShowModal] = useState(false);

    const handleCloseModal = () => setShowModal(false);
    const handleOpenModal = () => setShowModal(true);

    return (
        <>
            <Button variant="primary" onClick={handleOpenModal}>
                {btnName}
            </Button>

            <Modal show={showModal} onHide={handleCloseModal}>
                <Modal.Header closeButton>
                    <Modal.Title>{title}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {modalContent}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleCloseModal}>
                        Закрыть
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default ModalComponent;