import { getToken } from "./authManager";

const apiUrl = "/api/pc";

export const getAllPcs = () => {
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
                    "An unknown error occurred while trying to load PCs."
                );
            }
        });
    });
};

export const getPcDetails = (id) => {
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
                    "An unknown error occured while trying to get PC Details."
                );
            }
        });
    });
};

export const getUserPcs = () => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/userpcs`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error(
                    "An unknown error ocurred while trying to get user PCs"
                );
            }
        });
    });
};

export const editPc = (pc) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${pc.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(pc)
        }).then((res) => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error(
                    "An unknown error occured while editing a PC."
                );
            }
        });
    });
};

export const addPc = (pc) => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(pc)
        }).then((res) => {
            if (res.ok) {
                console.log("PC made successfully!")
                return res.json();
            } else {
                throw new Error(
                    "An error occured while trying to add a new PC."
                );
            }
        });
    });
};

export const deletePc = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    });
};