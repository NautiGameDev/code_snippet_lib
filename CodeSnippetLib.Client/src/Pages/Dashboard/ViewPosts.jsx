import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getPostSelection } from '../../services/postService';
import { getAllTags } from '../../services/tagService';

function ViewPosts() {
    const [errorMessage, setErrorMessage] = useState("");
    const [isLoading, setIsLoading] = useState(true);
    const [totalPosts, setTotalPosts] = useState(0);
    const [totalPages, setTotalPages] = useState(1);
    const [currentPage, setCurrentPage] = useState(1);
    const [postData, setPostData] = useState([]);
    const [tagsData, setTagsData] = useState([]);

    const [tag, setTag] = useState("");
    const [search, setSearch] = useState("");

    const postsPerPage = 20;

    const getTags = async () => {
        const tagsResponse = await getAllTags();

        if (tagsResponse.status !== 200) {
            setErrorMessage(`Error status ${tagsResponse.status}: ${tagsResponse.message}`);
        }

        setTagsData(tagsResponse.data.tags);
    }

    const getPosts = async (skip) => {
        setIsLoading(true);
        setErrorMessage("");

        const postsResponse = await getPostSelection(skip, postsPerPage, search, tag);

        if (postsResponse.status !== 200) {
            setErrorMessage(`Error status ${postsResponse.status}: ${postsResponse.message}`);
            setIsLoading(false);
            return;
        }

        if (postsResponse.data.count === 0) {
            setErrorMessage("No posts could be found with those search parameters.");
        }
        else {
            setPostData(postsResponse.data.posts);
            setTotalPosts(postsResponse.data.count);

            const calculatedPages = Math.ceil(postsResponse.data.count / postsPerPage);
            setTotalPages(calculatedPages);
        }


        setIsLoading(false);
    }

    const handleSubmitFilter = (e) => {
        e.preventDefault();

        const skip = (currentPage - 1) * postsPerPage;

        getPosts(skip);
    }

    const handlePageChange = (page) => {
        setCurrentPage(page);

        const skip = (page - 1) * postsPerPage;

        getPosts(skip);
    }

    useEffect(() => {      
        const initPage = () => {     
            getTags();
            getPosts(0);
        }

        initPage();
    }, []);

  return (
      <div className="view v-col-center">
          {!isLoading ? (
            <div className="col w-100 h-col-centered">
                  <form className="w-80 pt-16 pb-16 mb-16" onSubmit={(e) => handleSubmitFilter(e)} >
                      <div className="form-header">
                        <h3>Filters:</h3>
                      </div>
                      <div className="form-row pl-8">
                          <div className="form-col row">
                            <label>Search: </label>
                            <input className="w-75" type="text" value={search} placeholder="Search posts..." onChange={(e) => setSearch(e.target.value)}/>
                          </div>
                          <div className="form-col row">
                              <label>Tags: </label>
                              <select value={tag} onChange={(e) => setTag(e.target.value)}>
                                  <option default value="">--</option>

                                  {tagsData.map((tag, ti) => (
                                      <option key={ti} value={tag.name}>{tag.name}</option>
                                  ))}

                              </select>
                          </div>
                          <div className="form-col row">
                              <button className="btn-small" type="submit">Search</button>
                          </div>
                      </div>
                      
                  </form>

              <div className="card w-80 h-60vh pt-16 pb-16 glitch-in">
                <div className="row w-100 h-row-centered">
                      <h2>Posts</h2>
                  </div>
                  
                  <div className="row w-100 h-row-centered mb-16">
                      {errorMessage.trim() === "" ? (
                          `Total posts found: ${totalPosts}`                         
                      ) : (
                                  <span className="error">{errorMessage}</span>
                      )}
                  </div>
                  <table>
                      <thead>
                          <tr>
                              <th className="w-50">
                                Title
                              </th>
                              <th>
                                Tags
                              </th>
                              <th>
                                Last Updated
                              </th>
                          </tr>
                      </thead>
                      <tbody>
                      {postData.map((post, index) => (
                          <tr key={index} className={index % 2 === 0 ? "light-row" : "dark-row"}>
                              <td>
                                  {post.title}
                              </td>
                              <td>
                                  {post.tags.map((tag, ti) => (
                                      <span key={ti}>
                                          {tag}{ti === post.tags.length - 1 ? "" : ", "}
                                      </span>
                                  ))}
                              </td>
                              <td>
                                  {post.lastUpdated ? (
                                      
                                          new Date(post.lastUpdated).toLocaleString('en-US', {
                                              dateStyle: 'medium',
                                              timeStyle: 'short',
                                          })
                                      
                                  ): (                                          
                                        new Date(post.createdAt).toLocaleString('en-US', {
                                            dateStyle: 'medium',
                                            timeStyle: 'short',
                                        })                                          
                                      )}
                              </td>
                          </tr>
                      ))}
                      </tbody>
                  </table>
                  <div className="row w-100 h-row-centered mt-16">
                      {
                          Array.from({ length: totalPages }, (_, index) => (
                              <span className={currentPage === (index + 1) ? "page-link" : "page-link muted"} key={index} onClick={() => handlePageChange(index + 1)} >
                                  {index + 1}
                              </span> 
                          ))
                      }
                  </div>
                </div>
              </div>
          ): (
                  <h2>Loading posts...</h2>
              ) }
      </div>
  );
}

export default ViewPosts;