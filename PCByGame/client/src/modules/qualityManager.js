import { getToken } from "./authManager";

const apiUrl = "/api/quality"

export const getAllQualities = () => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occured while trying to get qualities."
                )
            }
        })
    })
}