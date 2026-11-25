import { GetActivityLogs } from "../../services/activityLogService";
import { useState, useEffect } from 'react';
function ActivityLogs() {
    const [sysMessage, setSysMessage] = useState("Fetching activity logs...");
    const [logs, setLogs] = useState([]);

    useEffect(() => {
        const getLogs = async () => {
            const response = await GetActivityLogs();

            if (response.status === 200) {
                setLogs(response.data.activityLogs);
                setSysMessage("");
            }
            else {
                setSysMessage(`Error ${response.status}: ${response.message}`);
            }
        }

        getLogs();
    }, []);

  return (
      <div className="view v-col-center">
          {sysMessage.trim() === "" ? (
              <div className="card w-75 h-80 glitch-in pt-16 pb-16">
                  <div className="row w-100 h-row-centered">
                    <h2>Activity Logs</h2>
                  </div>
                    <table>
                        <thead>
                            <tr>
                                  <th>
                                    UserName
                                  </th>
                                  <th>
                                    ActivityType
                                  </th>
                                  <th>
                                    Details
                                  </th>
                                  <th>
                                    TimeStamp
                                  </th>
                                  <th>
                                    Is Success
                                  </th>
                                </tr>
                              </thead>

                      {logs.map((log, index) => (
                          <tr key={index} className={index % 2 === 0 ? "light-row" : "dark-row"} >
                                  <td>
                                      {log.userName}
                                  </td>
                                  <td>
                                      {log.activityType}
                                  </td>
                                  
                                  <td>
                                      {log.details}
                                  </td>
                                  <td>
                                      {new Date(log.timeStamp).toLocaleString('en-US', {
                                          dateStyle: 'medium',
                                          timeStyle: 'short',
                                      })}
                                  </td>
                                  <td>
                                      {log.isSuccess ? "True" : "False"}
                                  </td>
                              </tr>
                          ))}
                  </table>
              </div>
          ) : (
                <h2 className="glitch-in">{sysMessage}</h2>
           )}
      </div>
  );
}

export default ActivityLogs;