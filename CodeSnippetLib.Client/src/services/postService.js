import { apiRequest } from '../Clients/apiClient';

export async function newPost(title, tags, content) {
    const body = {
        title: title,
        content: content,
        tags: tags.split(",")
    }

    return await apiRequest("post", 'POST', body);
}