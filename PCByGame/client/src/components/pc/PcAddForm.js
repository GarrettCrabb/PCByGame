import { useState } from 'react';
import { Button, Form, FormGroup, Input, Label } from "reactstrap"
import { useNavigate } from 'react-router-dom';
import { addPc } from '../../modules/pcManager';
import firebase from "firebase";
import "firebase/auth";
import { getUserDetails } from '../../modules/authManager';

const AddPcForm = () => {
    const navigate = useNavigate();

    const [name, setName] = useState();
    const [motherboard, setMotherboard] = useState();
    const [cpu, setCPU] = useState();
    const [ram, setRam] = useState();
    const [gpu, setGPU] = useState();
    const [psu, setPSU] = useState();
    const [storage, setStorage] = useState();
    const [caseName, setCaseName] = useState();
    const [cost, setCost] = useState();

    const submitPc = (e) => {
        e.preventDefault();
        const pc = {
            name,
            motherboard,
            cpu,
            ram,
            gpu,
            psu,
            storage,
            caseName,
            cost,
            userProfileId: null
        }
        // uses firebaseUserId to get userProfile then sets pc.userProfileId to userProfile.id
        const currentUser = firebase.auth().currentUser;
        if (currentUser) {
            getUserDetails(currentUser.uid).then((userProfile) => {
                pc.userProfileId = userProfile.id
                addPc(pc).then(() => navigate("/userpcs"))
            })
        }
    };

    return (
        <>
            <h1>New PC</h1>
            <Form onSubmit={submitPc}>
                <FormGroup>
                    <Label htmlFor="name">PC Name</Label>
                    <Input name="name"
                        type="text"
                        onChange={(e) => setName(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="cpu">CPU</Label>
                    <Input cpu="cpu"
                        type="text"
                        onChange={(e) => setCPU(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="gpu">GPU</Label>
                    <Input name="gpu"
                        type="text"
                        onChange={(e) => setGPU(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="motherboard">Motherboard</Label>
                    <Input name="motherboard"
                        type="text"
                        onChange={(e) => setMotherboard(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="ram">Ram</Label>
                    <Input name="ram"
                        type="text"
                        onChange={(e) => setRam(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="storage">Storage</Label>
                    <Input name="storage"
                        type="text"
                        onChange={(e) => setStorage(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="psu">PSU</Label>
                    <Input name="psu"
                        type="text"
                        onChange={(e) => setPSU(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="caseName">Case</Label>
                    <Input name="caseName"
                        type="text"
                        onChange={(e) => setCaseName(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="cost">Cost (include'$') (ex. $2,500)</Label>
                    <Input name="cost"
                        type="text"
                        onChange={(e) => setCost(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Button id="pc-save-button" color="success">Save</Button>
                </FormGroup>
            </Form>
        </>
    )
}

export default AddPcForm;