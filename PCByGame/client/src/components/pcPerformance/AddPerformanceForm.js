import { useEffect, useState } from 'react';
import { Button, Form, FormGroup, Input, Label } from "reactstrap"
import { useNavigate } from 'react-router-dom';
import { addPCPerformance } from '../../modules/pcPerformanceManager';
import { addPerformance } from '../../modules/performanceManager';
import { getAllGames } from '../../modules/gameManager';
import { getAllQualities } from '../../modules/qualityManager';
import { getUserPcs } from '../../modules/pcManager';
import { PcOption } from '../pc/PcOption';

const AddPerformanceForm = () => {
    const navigate = useNavigate();

    const [fps, setFPS] = useState();
    const [qualityId, setQualityId] = useState();
    const [gameId, setGameId] = useState();
    const [pcId, setPCId] = useState();
    const [pcs, setPCs] = useState([]);
    const [games, setGames] = useState([]);
    const [qualities, setQualities] = useState([]);

    const submitPerformance = (e) => {
        e.preventDefault();
        const performance = {
            fps,
            qualityId,
            gameId
        }

        // can I use .then to invoke addPCPerformance method and catch the ID of the new performance object in this form to also create the new PCPerformance object?
        addPerformance(performance).then((performanceId) => {
            const pcPerfomance = {
                performanceId,
                pcId
            }
            addPCPerformance(pcPerfomance).then(() => navigate("/userpcs"))
        })
    };

    useEffect(() => {
        getUserPcs().then((pcsArray) => {
            setPCs(pcsArray)
        })
    }, []);

    useEffect(() => {
        getAllGames().then((gamesArray) => {
            setGames(gamesArray)
        })
    }, []);

    useEffect(() => {
        getAllQualities().then((qualitiesArray) => {
            setQualities(qualitiesArray)
        })
    }, []);

    return (
        <>
            <h1>New Performance Stats</h1>
            <Form onSubmit={submitPerformance}>
                <FormGroup>
                    <Label htmlFor="pcId">PC</Label>
                    <Input name="pcId"
                        type="select"
                        onChange={(e) => setPCId(e.target.value)}>
                        <option>Select a PC</option>
                        {
                            pcs.map(
                                (pc) => {
                                    return <PcOption pcGetAllViewModel={pc}/>
                                }
                            )
                        }
                    </Input>
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="fps">FPS (Frames Per Second)</Label>
                    <Input name="fps"
                        type="text"
                        onChange={(e) => setFPS(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="qualityId">Quality Setting</Label>
                    <Input name="qualityId"
                        type="select"
                        onChange={(e) => setQualityId(e.target.value)}>
                        <option>Select a Quality Setting</option>
                        {
                            qualities.map(
                                (q) => {
                                    return <option key={`${q.id}`} value={q.id}>{q.name}</option>
                                }
                            )
                        }
                    </Input>
                </FormGroup>
                <FormGroup>
                    <Label htmlFor="gameId">Game</Label>
                    <Input name="gameId"
                        type="select"
                        onChange={(e) => setGameId(e.target.value)}>
                        <option>Select a Game</option>
                        {
                            games.map(
                                (g) => {
                                    return <option key={`${g.Id}`} value={g.id}>{g.name}</option>
                                }
                            )
                        }
                    </Input>
                </FormGroup>
                <FormGroup>
                    <Button id="performance-save-button" color="success">Save</Button>
                </FormGroup>
            </Form>
        </>

    )
}

export default AddPerformanceForm;