import { getToken } from "./authManager";

const apiUrl = "/api/performance";

export const editPerformance = (performance) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${performance.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(performance)
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occured while editing performance."
                );
            }
        });
    });
};

export const addPerformance = (performance) => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(performance)
        }).then((res) => {
            if (res.ok) {
                console.log("Performance made successfully!")
                return res.json();
            } else {
                throw new Error(
                    "An error occurred while trying to add new performance."
                );
            }
        });
    });
};