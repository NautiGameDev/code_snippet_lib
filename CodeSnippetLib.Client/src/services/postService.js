import { apiRequest } from '../Clients/apiClient';

export async function newPost(title, tags, content) {
    const body = {
        title: title,
        content: content,
        tags: tags.split(",")
    }

    return await apiRequest("post", 'POST', body);
}

export async function loadPost(slug) {
    return await apiRequest(`post/by-slug/${slug}`, 'GET', null);
}

export async function getPostSelection(skip, take, search = null, tag = null) {   

    let url = `post?skip=${skip}&take=${take}`;

    if (search && search.trim() !== "") {
        url += `&search=${search.trim()}`;
    }

    if (tag && tag.trim() !== "") {
        url += `&tag=${tag.trim()}`;
    }


    return await apiRequest(url, 'GET', null);
}

export async function updatePost(title, tags, content, slug) {
    const body = {
        title: title,
        content: content,
        tags: tags.split(",")
    }

    return await apiRequest(`post/edit-post/${slug}`, 'PUT', body);
}

export async function deletePost(slug) {
    return await apiRequest(`post/delete-post/${slug}`, 'DELETE', null);
}