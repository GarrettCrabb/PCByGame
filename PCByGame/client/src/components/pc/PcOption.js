export const PcOption = ({ pcGetAllViewModel }) => {
    return (
        <option key={pcGetAllViewModel?.pc?.id} value={pcGetAllViewModel?.pc?.id}>{pcGetAllViewModel?.pc?.name}</option>
    )
}
