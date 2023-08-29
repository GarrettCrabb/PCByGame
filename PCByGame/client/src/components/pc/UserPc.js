import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { Button, ButtonGroup, Card, CardBody, CardGroup, CardHeader, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import { deletePc } from "../../modules/pcManager";

export const UserPc = ({ pcGetAllViewModel }) => {
    const [modal, setModal] = useState(false);

    const confirmDelete = () => {
        deletePc(pcGetAllViewModel?.pc?.id)
            .then(res => {
                if (res.ok) {
                    setModal(false);
                    window.location.reload();
                }
            });
    };

    return (
        <Card className="m-5 text-center" style={{ 'borderRadius': '20px' }}>
            <CardHeader>
                <strong>{pcGetAllViewModel?.pc?.name}</strong>
            </CardHeader>
            <CardBody>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>CPU: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.cpu}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>GPU: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.gpu}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>Motherboard: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.motherboard}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>Ram: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.ram}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>Storage: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.storage}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>PSU: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.psu}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>Case: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.caseName}</p>
                </CardGroup>
                <CardGroup className="d-flex justify-content-center">
                    <strong style={{ 'marginRight': '5px' }}>Cost: </strong>
                    <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.pc?.cost}</p>
                </CardGroup>
                {pcGetAllViewModel?.pcPerformance !== null && (
                    <CardGroup className="d-flex justify-content-center">
                        <strong style={{ 'marginRight': '5px' }}>{pcGetAllViewModel?.game?.name}: </strong>
                        <p style={{ 'marginLeft': '5px' }}>{pcGetAllViewModel?.performance?.fps} FPS at {pcGetAllViewModel?.quality?.name} Quality</p>
                    </CardGroup>
                )}
                <ButtonGroup className="d-flex justify-content-center">
                    <Link to={`/editPc/${pcGetAllViewModel?.pc?.id}`}><Button color="primary">Edit PC</Button></Link>
                    <Link to={`/addperformance/${pcGetAllViewModel?.pc?.id}`}><Button color="primary">Add Performance Stats</Button></Link>
                    <Link><Button color="danger" onClick={() => setModal(!modal)}>Delete</Button></Link>
                </ButtonGroup>
                <Modal isOpen={modal} toggle={() => setModal(!modal)}>
                    <ModalHeader toggle={() => setModal(!modal)}>Confirm Delete</ModalHeader>
                    <ModalBody>
                        Are you sure you want to delete {pcGetAllViewModel?.pc?.name}
                    </ModalBody>
                    <ModalFooter>
                        <Button onClick={() => confirmDelete()} color="primary">Confirm</Button>
                        <Button onClick={() => setModal(!modal)} color="secondary">Cancel</Button>
                    </ModalFooter>
                </Modal>
            </CardBody>
        </Card>
    )
}

// Add performance values and check for null data with if (if PCPerformance === null <p>No Performance Data</p>) and show a button that routes to adding performance