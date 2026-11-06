import { apiRequest } from '../Clients/apiClient';

export async function newPost(title, tags, content) {
    const body = {
        title: title,
        content: content,
        tags: tags.split(",")
    }

    return await apiRequest("post", 'POST', body);
}

export async function getPostSelection(skip, take) {   

    return await apiRequest(`post?skip=${skip}&take=${take}`, 'GET', null);
}

export async function getPostCount() {
    return await apiRequest('post/get-count', 'GET', null);
}