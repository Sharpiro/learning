#![allow(dead_code)]

use std::{cell::RefCell, rc::Rc};

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
    // todo: can't borrow
    pub fn find(node_cell: Rc<RefCell<Node>>, value: i32) -> Option<Rc<RefCell<Node>>> {
        // todo: can't borrow once at top
        // let node = node_cell.borrow();
        if node_cell.borrow().value == value {
            // if node.value == value {
            return Some(node_cell);
        }

        if let Some(next) = &node_cell.borrow().next {
            let rc_next = Rc::clone(next);
            return Node::find(rc_next, value);
        }

        return None;
    }

    // pub fn find_inner(&self, value: i32) -> Option<Rc<RefCell<Node>>> {
    //     // todo: can't borrow once at top
    //     // let node = node_cell.borrow();
    //     if self.value == value {
    //         return Some(node_cell);
    //     }

    //     if let Some(next) = &node_cell.borrow().next {
    //         let rc_next = Rc::clone(next);
    //         return Node::find(rc_next, value);
    //     }

    //     return None;
    // }

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
    //         let mut sub_vec = next.to_vec_ref();
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

    // // fn last(&mut self) -> &mut Node {
    // fn last(&mut self) {
    //     let child = self.next.as_mut().unwrap();
    //     child.next = None;
    // }

    // fn push(&mut self, val: i32) {
    //     self.last()
    //     // let last = self.last();
    //     // last.next = Some(Box::from(Node {
    //     //     value: val,
    //     //     next: None,
    //     // }));
    //     // last.next = None;
    // }

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

// pub fn find_outer(node_cell: &Rc<RefCell<Node>>, value: i32) -> Option<Rc<RefCell<Node>>> {
//     find(Rc::clone(node_cell), value)
// }

pub fn get_test_list(size: i32, current: i32) -> Rc<RefCell<Node>> {
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

    // #[test]
    // fn swap_with_child_test() {
    //     let root = get_test_list(2, 1);
    //     println!("{:?}", root);
    //     {
    //         // root.swap_with_child();
    //         // let node = *root;
    //         // swap_with_child(&root);
    //         // println!("{:?}", child);
    //     }
    //     println!("{:?}", root);
    // }

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

        assert_eq!(Node::find(Rc::clone(&root), 1).unwrap().borrow().value, 1);
        assert_eq!(Node::find(Rc::clone(&root), 3).unwrap().borrow().value, 3);
        assert_eq!(Node::find(Rc::clone(&root), 0), None);
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
