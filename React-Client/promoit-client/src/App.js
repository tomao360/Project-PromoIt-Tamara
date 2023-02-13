import { useAuth0 } from "@auth0/auth0-react";
import { RingLoader } from "react-spinners";

import { Main } from "./components/main/main.component";
import { UnauthorizedUsers } from "./components/unauthorizedUsersPage/unauthorized.users.page.component";

import "./App.css";

function App() {
  const { isAuthenticated, isLoading } = useAuth0();
  if (!isLoading) {
    return isAuthenticated ? <Main /> : <UnauthorizedUsers className="app" />;
  } else {
    return (
      <div className="spinner-app">
        <RingLoader color="#414141" size={300} />
      </div>
    );
  }
}

export default App;
