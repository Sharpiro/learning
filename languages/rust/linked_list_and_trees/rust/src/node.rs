#![allow(dead_code)]

use std::{cell::RefCell, mem, ptr, rc::Rc};

// #![allow(unused_variables)]

#[derive(Debug, PartialEq)]
pub struct Node {
    pub value: i32,
    pub next: Option<Rc<RefCell<Node>>>,
    // pub next: Option<Rc<Node>>,
    // pub next: Option<Box<Node>>,
    // pub next: Option<*const Node>,
    // pub next: Option<&'a Node<'a>>,
    // pub next: Option<Node>,
}

impl Drop for Node {
    fn drop(&mut self) {
        println!("dropping node: {}", self.value);
    }
}

impl Node {
    fn find_internal(node_cell: Rc<RefCell<Node>>, value: i32) -> Option<Rc<RefCell<Node>>> {
        // todo: can't borrow once at top
        // let node = node_cell.borrow();
        if node_cell.borrow().value == value {
            return Some(node_cell);
        }

        if let Some(next) = &node_cell.borrow().next {
            let rc_next = Rc::clone(next);
            return Node::find_internal(rc_next, value);
        }

        return None;
    }

    pub fn to_vec(&self) -> Vec<i32> {
        let mut vec = vec![self.value];
        if let Some(next) = &self.next {
            let mut temp = next.borrow().to_vec();
            vec.append(&mut temp);
        };
        vec
    }

    // fn to_vec_ref(&self) -> Vec<&Node> {
    //     let mut vec = vec![self];
    //     if let Some(next) = &self.next {
    //         let mut sub_vec = next.borrow().to_vec_ref();
    //         vec.append(&mut sub_vec);
    //     }
    //     vec
    // }

    // fn swap_with_child(&self) {
    //     // self.next = None;
    //     // fn swap_with_child(&mut self) -> &Node {
    //     // let child = self.next.as_ref().unwrap();
    //     // child.next = Some(self);
    //     // self.next = None;
    //     // child.next = None;
    //     // let wannabe = Box::from(self);
    //     // let wannabe2 = Some(wannabe);
    //     // child
    //     // self
    // }

    fn last_internal(node: Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        if let Some(next_cell) = &node.borrow().next {
            let next = Rc::clone(&next_cell);
            Node::last_internal(next)
        } else {
            Rc::clone(&node)
        }
    }

    fn push_internal(node: Rc<RefCell<Node>>, value: i32) -> Rc<RefCell<Node>> {
        // note: rust not smart enough to drop borrow in else block
        // if let Some(next_cell) = &node.borrow().next {
        //     let next = Rc::clone(next_cell);
        //     return Node::push_internal(next, value);
        // }

        let last_node = Node::last(&node);
        let new_node = Rc::from(RefCell::from(Node { value, next: None }));
        last_node.borrow_mut().next = Some(Rc::clone(&new_node));
        new_node
    }

    fn pop_internal(node_cell: Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        // let node = node_cell.borrow();
        let mut node = node_cell.borrow_mut();
        let next_node = node.next.as_ref().expect("no good");
        let is_last_node = next_node.borrow().next.as_ref().is_none();
        if !is_last_node {
            let next_rc = Rc::clone(next_node);
            return Node::pop_internal(next_rc);
        }

        // todo: can this be done w/o braces or drop function?
        // maybe not, drop is idiomatic for smart pointers managing locks
        // JK just borrow mutably once at start instead of immutable > drop > mutable
        // drop(node);
        // node.next = None;
        let popped = node.next.take().unwrap();
        // let popped = mem::replace(&mut node.next, None).unwrap();
        // let popped = unsafe {
        //     let result = ptr::read(&node.next);
        //     ptr::write(&mut node.next, None);
        //     result.unwrap()
        // };
        // node_cell.borrow_mut().next = None;
        // node.next = None;
        popped
    }

    // pub fn reverse(&mut self) -> &Node {
    // pub fn reverse(&mut self) {
    //     // let temp: &Node;
    //     {
    //         let mut vec_ref = self.to_vec_ref();
    //         // vec_ref
    //         for (_, node) in vec_ref.iter_mut().skip(1).enumerate().rev() {
    //             node.next = None;
    //             dbg!(node.value);
    //         }
    //         // temp = vec_ref[vec_ref.len() - 1];
    //     }
    //     // self.next = None;
    // }
}

impl Node {
    pub fn find(node_cell: &Rc<RefCell<Node>>, value: i32) -> Option<Rc<RefCell<Node>>> {
        Node::find_internal(Rc::clone(node_cell), value)
    }

    pub fn last(node_cell: &Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        Node::last_internal(Rc::clone(node_cell))
    }

    pub fn push(node_cell: &Rc<RefCell<Node>>, value: i32) -> Rc<RefCell<Node>> {
        Node::push_internal(Rc::clone(node_cell), value)
    }

    pub fn pop(node_cell: &Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        Node::pop_internal(Rc::clone(node_cell))
    }
}

fn get_test_list(size: i32, current: i32) -> Rc<RefCell<Node>> {
    let mut node = Node {
        value: current,
        next: None,
    };
    if current < size {
        let next = get_test_list(size, current + 1);
        node.next = Some(next);
    }
    Rc::from(RefCell::new(node))
}

#[cfg(test)]
mod tests {
    use crate::node::*;

    #[test]
    fn test_to_vec() {
        let root = get_test_list(5, 1);
        let vec = root.borrow().to_vec();

        assert_eq!(vec.len(), 5);
        assert_eq!(vec[0], 1);
        assert_eq!(vec[1], 2);
        assert_eq!(vec[2], 3);
        assert_eq!(vec[3], 4);
        assert_eq!(vec[4], 5);
    }

    #[test]
    fn test_find() {
        let root = get_test_list(5, 1);
        assert_eq!(Node::find(&root, 1).unwrap().borrow().value, 1);
        assert_eq!(Node::find(&root, 3).unwrap().borrow().value, 3);
        assert_eq!(Node::find(&root, 0), None);
    }

    #[test]
    fn test_push() {
        let root_cell = get_test_list(3, 1);
        let old_last_node = Node::last(&root_cell);
        let new_node = Node::push(&root_cell, 4);
        let last_node = Node::last(&root_cell);

        let last_node_ptr: *const Node = &*last_node.borrow();
        let new_node_ptr: *const Node = &*new_node.borrow();
        assert_ne!(old_last_node, new_node);
        assert_eq!(last_node, new_node);
        assert_eq!(last_node_ptr, new_node_ptr);
        assert_eq!(new_node.borrow().value, 4);
    }

    #[test]
    fn test_pop() {
        let root_cell = get_test_list(3, 1);
        println!("{:?}", root_cell);
        let popped = Node::pop(&root_cell);
        // let old_last_node = Node::last(&root_cell);
        // let new_node = Node::push(&root_cell, 4);
        // let last_node = Node::last(&root_cell);
        println!("{:?}", root_cell);
        println!("{:?}", popped);

        assert_eq!(root_cell.borrow().to_vec(), [1, 2]);
        assert_eq!(popped.borrow().to_vec(), [3]);
    }

    // #[test]
    // fn test_multi_own_list() {
    //     let a = Rc::from(1);
    //     let b = Rc::new(1);
    //     let a_node = Rc::from(RefCell::from(Node {
    //         value: 5,
    //         next: Some(Rc::from(RefCell::from(Node {
    //             value: 10,
    //             next: None,
    //         }))),
    //     }));
    //     let b_node = Node {
    //         value: 3,
    //         next: Some(Rc::clone(&a_node)),
    //     };
    //     let c_node = Node {
    //         value: 4,
    //         next: Some(Rc::clone(&a_node)),
    //     };

    //     // a_node.borrow_mut().value = 99;
    //     println!("{:?}", a_node);
    //     println!("{:?}", b_node);
    //     println!("{:?}", c_node);
    // }

    // #[test]
    // fn test_reverse() {
    //     let mut root = get_test_list(5, 1);
    //     println!("{:?}", root);
    //     {
    //         // let reversed = root.reverse();
    //         // let vec = reversed.to_vec();
    //         root.reverse();
    //         // let vec = root.to_vec();
    //         // println!("{:?}", reversed);
    //     }

    //     println!("{:?}", root);
    //     // assert_eq!(vec.len(), 5);
    //     // assert_eq!(vec[0], 5);
    //     // assert_eq!(vec[1], 4);
    //     // assert_eq!(vec[2], 3);
    //     // assert_eq!(vec[3], 2);
    //     // assert_eq!(vec[4], 1);
    // }
}
