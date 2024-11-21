import React, { useState, useEffect } from 'react'
import configuration from '../../configurations/configuration';
import { fetchProducts } from '../../util/apis/productApi';
import Carousel from '../../util/carousel/Carousel';
import './product.css';

const Products = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchQuery, setSearchQuery] = useState(''); 
    const [filteredProducts, setFilteredProducts] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
          try {
            const productData = await fetchProducts(configuration.products);  //calling api
            setProducts(productData);
            setFilteredProducts(productData);
          } catch (err) {
            setError(err.message);
          } finally {
            setLoading(false);
          }
        };
    
        fetchData();
      }, []);


      const handleSearch = (e) => {
        const query = e.target.value;
        setSearchQuery(query);
    
        // Filter products based on the search query
        if (query.trim() === '') {
          setFilteredProducts(products); // If query is empty, show all products
        } else {
          const filtered = products.filter((product) =>
            product.title.toLowerCase().includes(query.toLowerCase())
          );
          setFilteredProducts(filtered);
        }
      };

    return (
      <React.Fragment>


<div className="text-center mb-3">
  <h2 className="text-dark mt-3">Products</h2>
  <div className="d-flex justify-content-end mt-2">
    <input
      type="text"
      className="form-control search-text w-25"
      placeholder="Search products..."
      value={searchQuery}
      onChange={handleSearch} // Call handleSearch on input change
    />
  </div>
</div>
<div className="card-container">
  {loading && (
    <div className="centered-message">
      <p>Loading...</p>
    </div>
  )}
  {!loading && error && (
    <div className="centered-message">
      <p className="text-danger">Error: {error}</p>
    </div>
  )}

  {/* Show filtered products if there are any and no errors */}
  {!loading && !error && filteredProducts.length > 0 && (
    filteredProducts.map((product, index) => (
      <div key={index} className="cardOuterdiv">
        <div className="card card-border">
          <Carousel images={product.images} index={index} />
          <div className="card-body">
            <h6 className="card-title fw-bold">{product.title} (€{product.price})</h6>
            <div className="d-flex">
              <div className="flex-grow-1">
                <p className="card-text">{product.description}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    ))
  )}

  {/* Show "No products available" if there are no products and no error */}
  {!loading && !error && filteredProducts.length === 0 && (
    <div className="centered-message">
      <p>No products available</p>
    </div>
  )}
</div>


  </React.Fragment>
)
}
export default Products