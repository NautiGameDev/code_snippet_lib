import { apiRequest } from "../Clients/apiClient";


export async function getAllTags() {
    return await apiRequest("tag", 'GET', null); 
}