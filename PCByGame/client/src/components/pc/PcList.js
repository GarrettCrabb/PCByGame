import React, { useEffect, useState } from 'react';
import { Pc } from './Pc';
import { getAllPcs } from '../../modules/pcManager';

export const PcList = () => {
    const [pcs, setPcs] = useState([]);

    useEffect(() => {
        getAllPcs().then(setPcs);
    }, []);
    
    return (
        <>
            <h1 className='text-center'>All PCs</h1>
            <section>
                {pcs.map((p) => (
                    <Pc key={p.id} pcGetAllViewModel={p} />
                ))}
            </section>
        </>
    )
}