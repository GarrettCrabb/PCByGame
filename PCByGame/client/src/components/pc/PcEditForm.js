import { useEffect, useState } from 'react';
import { Button, Form, FormGroup, Input, Label } from "reactstrap"
import { useNavigate, useParams } from 'react-router-dom';
import { editPc, getPcDetails } from '../../modules/pcManager';
import firebase from "firebase";
import "firebase/auth";
import { getUserDetails } from '../../modules/authManager';

const EditPcForm = () => {
    const navigate = useNavigate();
    const { id } = useParams();

    
    
    const [ pc, updatePc ] = useState({
        name: "",
        motherboard: "",
        cpu: "",
        ram: "",
        gpu: "",
        psu: "",
        storage: "",
        caseName: "",
        cost: "",
        userProfileId: null
    })

    useEffect(() => {
        getPcDetails(id).then(updatePc);
    }, []);
      
const submitPc = (e) => {
    e.preventDefault();
    
    // uses firebaseUserId to get userProfile then sets pc.userProfileId to userProfile.id
    const currentUser = firebase.auth().currentUser;
    if (currentUser) {
        getUserDetails(currentUser.uid).then((userProfile) => {
            pc.userProfileId = userProfile.id
            editPc(pc).then(() => navigate("/userpcs"))
        })
    }
};

    return (
        <>
            <h1>Edit PC</h1>
            <Form onSubmit={submitPc}>
                <FormGroup>
                    <Label htmlFor="name">PC Name</Label>
                    <Input name="name"
                        type="text"
                        value={pc.Name}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.name = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="cpu">CPU</Label>
                    <Input cpu="cpu"
                        type="text"
                        value={pc.cpu}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.cpu = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="gpu">GPU</Label>
                    <Input name="gpu"
                        type="text"
                        value={pc.gpu}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.gpu = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="motherboard">Motherboard</Label>
                    <Input name="motherboard"
                        type="text"
                        value={pc.motherboard}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.motherboard = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="ram">Ram</Label>
                    <Input name="ram"
                        type="text"
                        value={pc.ram}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.ram = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="storage">Storage</Label>
                    <Input name="storage"
                        type="text"
                        value={pc.storage}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.storage = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="psu">PSU</Label>
                    <Input name="psu"
                        type="text"
                        value={pc.psu}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.psu = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="caseName">Case</Label>
                    <Input name="caseName"
                        type="text"
                        value={pc.caseName}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.caseName = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="cost">Cost (include'$') (ex. $2,500)</Label>
                    <Input name="cost"
                        type="text"
                        value={pc.cost}
                        onChange={
                            (e) => {
                                const copy = { ...pc }
                                copy.cost = e.target.value
                                updatePc(copy)
                            }
                        } />
                </FormGroup>
                <FormGroup>
                    <Button id="pc-save-button" color="success">Save</Button>
                </FormGroup>
            </Form>
        </>
    )
}

export default EditPcForm;