import React, { useState, useEffect } from "react";
import { RingLoader } from "react-spinners";

import {
  getMessagesData,
  deleteMessageFromDb,
} from "../../../services/userMessage.services";

import "./style.css";

export const UserMessages = (props) => {
  const [messageArr, setMessageArr] = useState(undefined);

  //initialize messages array
  const initMessagesData = async () => {
    let messages = await getMessagesData();
    console.log(messages);
    let messagesObject = Object.values(messages);
    setMessageArr(messagesObject);
  };

  useEffect(() => {
    initMessagesData();
  }, []);

  //delete message from database by MessageID
  const handleDeleteMessage = async (MessageID) => {
    await deleteMessageFromDb(MessageID);
    let newMessagesArr = messageArr.filter((m) => m.MessageID !== MessageID);
    setMessageArr(newMessagesArr);
  };

  return (
    <div className="messages-container">
      <div>
        <h3 className="title-message">Users Messages</h3>
      </div>
      <div className="table-container">
        <table className="table table-hover">
          <thead className="table-secondary">
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Email</th>
              <th scope="col">User Message</th>
              <th scope="col"></th>
            </tr>
          </thead>
          {messageArr && messageArr !== undefined ? (
            messageArr.map((m) => {
              let { MessageID, Name, Email, UserMessage } = m;
              return (
                <tbody>
                  <tr>
                    <td>{Name}</td>
                    <td>{Email}</td>
                    <td>{UserMessage}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-danger"
                        data-bs-toggle="modal"
                        data-bs-target={`#staticBackdrop${MessageID}`}
                      >
                        Delete User Message
                      </button>
                      <div
                        className="modal fade"
                        id={`staticBackdrop${MessageID}`}
                        data-bs-backdrop="static"
                        data-bs-keyboard="false"
                        tabindex="-1"
                        aria-labelledby="staticBackdropLabel"
                        aria-hidden="true"
                      >
                        <div className="modal-dialog">
                          <div className="modal-content">
                            <div className="modal-header">
                              <h1
                                className="modal-title fs-5"
                                id="staticBackdropLabel"
                              >
                                Delete User Message
                              </h1>
                              <button
                                type="button"
                                className="btn-close"
                                data-bs-dismiss="modal"
                                aria-label="Close"
                              ></button>
                            </div>
                            <div className="modal-body">
                              Are you sure you want to delete the user message?
                            </div>
                            <div className="modal-footer">
                              <button
                                type="button"
                                className="btn btn-secondary"
                                data-bs-dismiss="modal"
                              >
                                No
                              </button>
                              <button
                                type="button"
                                className="btn btn-danger"
                                onClick={() => {
                                  handleDeleteMessage(MessageID);
                                }}
                                data-bs-dismiss="modal"
                              >
                                Delete
                              </button>
                            </div>
                          </div>
                        </div>
                      </div>
                    </td>
                  </tr>
                </tbody>
              );
            })
          ) : (
            <tbody>
              <tr>
                <td colSpan={9}>
                  <RingLoader className="spinner" color="#414141" />
                </td>
              </tr>
            </tbody>
          )}
        </table>
      </div>
    </div>
  );
};
