import React, { useEffect, useState } from 'react';
import { AddProduct } from './AddProduct'; // Assuming AddProduct is in the same folder

const ProductList = () => {
  const [products, setProducts] = useState([]); // For storing products
  const [loading, setLoading] = useState(true); // For loading state
  const [error, setError] = useState(null); // For error state
  const [showModal, setShowModal] = useState(false); // For modal visibility state

  // Fetch products from API when the component mounts
  useEffect(() => {
    // Simulating an API call with a timeout (replace with actual API call)
    setTimeout(() => {
      const fetchedProducts = [
        {
          id: 1,
          name: 'Product 1',
          price: 100,
          dateAdded: '2025-04-01',
          quantity: 10,
          description: 'Description of Product 1',
          imageUrl: 'https://placekitten.com/150/150'
        },
        {
          id: 2,
          name: 'Product 2',
          price: 200,
          dateAdded: '2025-04-02',
          quantity: 15,
          description: 'Description of Product 2',
          imageUrl: 'https://via.placeholder.com/150'
        },
        {
          id: 3,
          name: 'Product 3',
          price: 300,
          dateAdded: '2025-04-03',
          quantity: 20,
          description: 'Description of Product 3',
          imageUrl: 'https://via.placeholder.com/150'
        }
      ];
      setProducts(fetchedProducts);
      setLoading(false);
    }, 2000);
  }, []);

  // Toggle the modal visibility
  const toggleModal = () => {
    setShowModal(!showModal);
  };

  // Handle delete (dummy function)
  const handleDelete = (productId) => {
    if (window.confirm('Are you sure you want to delete this product?')) {
      setProducts(products.filter((product) => product.id !== productId));
    }
  };

  if (loading) {
    return <div>Loading products...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className="container mt-5">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Product List</h2>

        {/* Add product button */}
        <div className="mb-4">
          <button className="btn btn-primary" onClick={toggleModal}>
            Add New Product
          </button>
        </div>
      </div>

      {/* Modal */}
      {showModal && (
        <div
          className="modal fade show"
          style={{ display: 'block' }}
          tabIndex="-1"
          role="dialog"
          aria-hidden="false"
        >
          <div className="modal-dialog modal-dialog-centered" role="document" style={{ maxWidth: '700px' }}>
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Add New Product</h5>
                <button
                  type="button"
                  className="close"
                  onClick={toggleModal}
                  aria-label="Close"
                >
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div className="modal-body">
                <AddProduct className="w-100" />
              </div>
            </div>
          </div>
        </div>
      )}


      {/* Background overlay */}
      {showModal && (
        <div
          className="modal-backdrop fade show"
          onClick={toggleModal} // Close modal if backdrop is clicked
        ></div>
      )}

      {/* Product Table */}
      <div className="table-responsive">
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Name</th>
              <th>Price</th>
              <th>Date Added</th>
              <th>Quantity</th>
              <th>Description</th>
              <th>Image</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {products.map((product) => (
              <tr key={product.id}>
                <td>{product.name}</td>
                <td>${product.price}</td>
                <td>{new Date(product.dateAdded).toLocaleDateString()}</td>
                <td>{product.quantity}</td>
                <td>{product.description}</td>
                <td>
                  <img src={product.imageUrl} alt={product.name} width="50" />
                </td>
                <td className="d-flex gap-1">
                  <button className="btn btn-warning btn-sm mr-2">Edit</button>
                  <button className="btn btn-danger btn-sm">Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ProductList;
