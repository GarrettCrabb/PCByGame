import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom"
import { getUserPcs } from "../../modules/pcManager";
import { UserPc } from "./UserPc";
import { Button } from "reactstrap";

export const UserPcList = () => {
    const [pcs, setPcs] = useState([]);

    useEffect(() => {
        getUserPcs().then(setPcs);
    }, []);

    if (pcs.length > 0) {
        return (
            <>
                <h1 className='text-center'>Your PCs</h1>
                <section>
                    <Link to="/addPc"><Button color="primary">Add PC</Button></Link>
                    {pcs.map((p) => (
                        <UserPc key={p.id} pcGetAllViewModel={p} />
                    ))}
                </section>
            </>
        )
    } else {
        return (
            <>
                <p>You have no PCs yet.</p>
                <p>Click <Link to="/addPc">here</Link> to make your first PC!</p>
            </>
        )
    }
}

// make sure to change link to go to /addPc