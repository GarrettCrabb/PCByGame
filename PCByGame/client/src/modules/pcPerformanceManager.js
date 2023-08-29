import { getToken } from "./authManager";

const apiUrl = "/api/pcperformance";

export const getPCPerformanceByPCId = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occured while trying to get PCPerformance."
                );
            }
        });
    });
};

export const editPCPerformance = (pcPerformance) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${pcPerformance.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(pcPerformance)
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occurred while editing PCPerformance."
                );
            }
        });
    });
};

export const addPCPerformance = (pcPerformance) => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(pcPerformance)
        }).then((res) => {
            if (res.ok) {
                console.log("PCPerformance made successfully!")
            } else {
                throw new Error(
                    "An error occured while trying to add new PCPerformance."
                );
            }
        });
    });
};