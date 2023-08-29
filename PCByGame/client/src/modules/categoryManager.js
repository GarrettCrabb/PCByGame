import { getToken } from "./authManager";

const apiUrl = "/api/category"

export const getAllCategories = () => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get categories."
                );
            }
        });
    });
};