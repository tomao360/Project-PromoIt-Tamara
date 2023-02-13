import React, { useState } from "react";
import { ToastContainer } from "react-toastify";

import { toastError, toastSuccess } from "../../constant/toastify";
import { addMessageToDb } from "../../services/userMessage.services";

import "./style.css";

export const ContactUs = ({ Email }) => {
  const [message, setMessage] = useState({
    Name: "",
    Email: Email,
    UserMessage: "",
  });
  //state to store the success status of adding message
  const [charactersLeft, setCharactersLeft] = useState(500);

  //add message to database
  const handleAddMessage = async () => {
    let json = message;
    if (!message.Name) {
      toastError("You must enter name");
    } else if (!message.UserMessage) {
      toastError("You must enter user message");
    } else {
      await addMessageToDb(json);
      toastSuccess("The message accepted successfully");
      setMessage({});
      document.querySelectorAll("input").forEach((input) => (input.value = ""));
      document
        .querySelectorAll("textarea")
        .forEach((textarea) => (textarea.value = ""));
    }
  };

  return (
    <div>
      <h1 className="contactUS-h1">Contact Us</h1>
      <p className="p-contactUS">
        We're here to help and answer any question you might have <br />
        Fill the form below and we will get back to you 😊
      </p>
      <div className="contactUS-container">
        <ToastContainer />
        <div className="mb-3 ">
          <label htmlFor="basic-url" className="form-label">
            Name
          </label>
          <input
            type="text"
            className="form-control"
            id="basic-url"
            aria-describedby="basic-addon3"
            placeholder="Name"
            onChange={(o) => {
              setMessage({
                ...message,
                Name: o.target.value,
              });
            }}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="Email1" className="form-label">
            Email address
          </label>
          <input
            readOnly
            type="email"
            className="form-control email-input"
            id="Email1"
            aria-describedby="emailHelp"
            value={Email}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="Textarea1" className="form-label">
            Message
          </label>
          <textarea
            className="form-control area-text"
            id="Textarea1"
            rows="3"
            onChange={(o) => {
              setMessage({ ...message, UserMessage: o.target.value });
              //setCharactersLeft within the textarea to 500 - o.target.value.length
              setCharactersLeft(500 - o.target.value.length);
            }}
            onKeyDown={(o) => {
              //textarea can have 500 characters if over 500 the preventDefault function activates
              if (o.target.value.length >= 500) {
                o.preventDefault();
              }
            }}
            //the max length of the textarea is 500 characters
            maxLength={500}
          ></textarea>
          <div>Characters left:{charactersLeft}</div>
        </div>
        <div className="send-button">
          <button className="btn btn-success" onClick={handleAddMessage}>
            Submit
          </button>
        </div>
      </div>
    </div>
  );
};
