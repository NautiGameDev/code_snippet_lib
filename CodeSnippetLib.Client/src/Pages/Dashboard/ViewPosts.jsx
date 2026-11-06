import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getPostCount, getPostSelection } from '../../services/postService';

function ViewPosts() {
    const [sysMessage, setSysMessage] = useState("Loading posts...");
    const [totalPosts, setTotalPosts] = useState(0);
    const [totalPages, setTotalPages] = useState(1);
    const [currentPage, setCurrentPage] = useState(1);
    const [postData, setPostData] = useState([]);

    const postsPerPage = 20;


    const calcPageCount = async () => {
        const countResponse = await getPostCount();

        if (countResponse.status !== 200) {
            setSysMessage("Error loading page count");
            return;
        }

        setTotalPosts(countResponse.data.count);

        const calculatedPages = Math.ceil(countResponse.data.count / postsPerPage);
        setTotalPages(calculatedPages);
    }

    const getPosts = async (skip) => {
        const postsResponse = await getPostSelection(skip, postsPerPage);

        if (postsResponse.status !== 200) {
            setSysMessage(`Error status ${postsResponse.status}: ${postsResponse.message}`);
            return;
        }

        if (postsResponse.data.posts.count === 0) {
            setSysMessage("No posts could be found.");
        }
        else {
            setPostData(postsResponse.data.posts);
            setSysMessage("");
        }
    }

    const handlePageChange = (page) => {
        setCurrentPage(page);

        const skip = (page - 1) * postsPerPage;

        getPosts(skip);
    }

    useEffect(() => {      
        const initPage = () => {
            calcPageCount();
            getPosts(0);
        }

        initPage();
    }, []);

  return (
      <div className="view glitch-in">
          {sysMessage.trim() === "" ? (
              <div className="card w-75 h-row-centered glitch-in">
                <div className="row w-100 h-row-centered">
                      <h2>Posts</h2>
                  </div>
                  <div className="row w-100 h-row-centered mb-16">
                      Filters go here
                  </div>
                  <table>
                      <thead>
                          <tr>
                              <th>
                                Title
                              </th>
                              <th>
                                Tags
                              </th>
                              <th>
                                Last Updated
                              </th>
                              <th>
                                Controls
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
                              <td>

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
          ): (
                  <h2>{sysMessage}</h2>
              ) }
      </div>
  );
}

export default ViewPosts;