import { useState } from 'react';
import { newPost } from '../../services/postService';
import MarkdownRenderer from '../../components/MarkdownRenderer';

function Write() {
    const [title, setTitle] = useState("");
    const [tags, setTags] = useState("");
    const [content, setContent] = useState("");

    const [isEditing, setIsEditing] = useState(true);

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (title.trim() === "" || tags.trim() === "" || content.trim() === "") {
            alert("Post is missing content");
            return;
        }

        const response = await newPost(title, tags, content);

        if (response.status === 201) {
            alert("Post created");
            setTitle("");
            setTags("");
            setContent("");
        }
        else {
            alert(`Error ${response.status}: ${response.message}`);
        }
    }

  return (
      <div className="view v-col-center glitch-in">

          {isEditing &&

              <form className="w-75 h-80 pt-16 pb-16 pl-16 pr-16" onSubmit={(e) => (handleSubmit(e))}>
                  <div className="form-header">
                      <h1>New Post</h1>
                  </div>
                  <div className="form-row">
                      <label htmlFor="title">Title:</label>
                      <input id="title" className="w-75" placeholder="Enter a title" value={title} onChange={(e) => setTitle(e.target.value)} ></input>
                  </div>
                  <div className="form-row">
                      <label htmlFor="tags">Tags:</label>
                      <input id="tags" className="w-75" placeholder="Enter tags separated by commas" value={tags} onChange={(e) => setTags(e.target.value)} ></input>
                  </div>
                  <div className="form-row h-75">
                      <div className="form-col h-100">
                          <label htmlFor="content">Content:</label>
                          <textarea id="content" placeholder="Write post" value={content} onChange={(e) => setContent(e.target.value)}></textarea>
                      </div>
                  </div>
                  <div className="form-row h-row-centered g-32 mt-16">
                      <button type="button" onClick={() => setIsEditing(false)}>Preview</button>
                          <button type="submit">Submit</button>                    
                  </div>
              </form>

          }

          {!isEditing &&
              <div className="card w-75 h-75 pt-16 pb-16 pl-16 pr-16">
                  <div className="row h-row-centered">
                    <h2>Previewing Content</h2>
                  </div>

                  <MarkdownRenderer content={content}></MarkdownRenderer>
                  <div className="row h-row-centered">
                      <button type="button" onClick={() => setIsEditing(true)}>Edit</button>
                  </div>
              </div>
          }
      </div>
  );
}

export default Write;