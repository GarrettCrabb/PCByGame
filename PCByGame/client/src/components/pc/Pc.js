import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { Card, CardBody, CardGroup, CardHeader } from "reactstrap";

export const Pc = ({ pcGetAllViewModel }) => {
    return (
        <Card className="m-5 text-center" style={{ 'borderRadius': '20px' }}>
            <CardHeader>
                <strong>{pcGetAllViewModel?.pc?.userProfile?.displayName}'s Pc</strong>
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
            </CardBody>
        </Card>
    )
}

// Add performance values and check for null data with if (if PCPerformance === null <p>No Performance Data</p>)